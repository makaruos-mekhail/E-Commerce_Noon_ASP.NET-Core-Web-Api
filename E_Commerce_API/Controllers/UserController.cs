using AutoMapper;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace E_Commerce_API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;
       
        public UserController(DContext context,
            UserManager<User> userManager
            ,SignInManager<User> signInManager,
            RoleManager<IdentityRole<long>> roleManager,
            ILogger<UserController> logger,
            IMapper mapper,
            IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
        _httpContext = httpContext.HttpContext;
        }
        [HttpPost]
        public async Task<IActionResult> Registeration([FromBody] UserDto userdto)
        {

            _logger.LogInformation($"Registration Attempt For {userdto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var user = _mapper.Map<User>(userdto);
                user.UserName = user.Email;

                var result = await _userManager.CreateAsync(user,userdto.Password);

                bool UserRoleIsExists = await _roleManager.RoleExistsAsync("Customer");
                if (!UserRoleIsExists)
                {
                    _logger.LogInformation("Adding CUSTOMER role");

                    var roleResult =await _roleManager.CreateAsync(new IdentityRole<long> { Name = "Customer" });

                    if (!roleResult.Succeeded)
                    {
                        _logger.LogError("Error in creating CUSTOMER role");
                        return BadRequest("Error in creating CUSTOMER role");
                    }
                }
                await _userManager.AddToRoleAsync(user, "Customer");

                if (!result.Succeeded)
                {
                    // if user found DuplicateUserName
                    if (result.Errors.Any(e => e.Code == "DuplicateUserName"))
                    {
                        return BadRequest("Email already in use");
                    }

                    return BadRequest("error");
                }
                else
                {
                    WishList wishList=new WishList() {UserId= user.Id};
                   await _context.WishList.AddAsync(wishList);
                   await _context.SaveChangesAsync();
                    return Ok();
                }

                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"somethoing went wrong in the {nameof(Registeration)}");
                return Problem($"somethoing went wrong in the {nameof(Registeration)}", statusCode: 500);
            }
           

        }
        [HttpPost]
        public async Task<ResultModel> SignIn([FromBody] LoginUserDto model)
        {
            ResultModel myModel = new ResultModel();
            if (ModelState.IsValid == false)
            {
                myModel.Success = false;
                myModel.Data =
                    ModelState.Values.SelectMany
                            (i => i.Errors.Select(x => x.ErrorMessage));
            }
            else
            {
                var result
                     = await _signInManager.PasswordSignInAsync
                        (model.Email, model.Password ,false,false);
                var user1 = await _userManager.FindByNameAsync(model.Email);

                if (user1 is null || !await _userManager.CheckPasswordAsync(user1, model.Password))
                {
                    myModel.Success = false;
                    myModel.Message = "Invalid UserName Or Password .";
                }
                else if (result.IsNotAllowed == true)
                {
                    myModel.Success = false;
                    myModel.Message = "Invalid UserName Or Password ";
                }
                else if (result.IsLockedOut)
                {
                    myModel.Success = false;
                    myModel.Message = "Is Locked Out";
                }


                else
                {
                    var user = await _userManager.FindByNameAsync(model.Email);
                    List<Claim> claims = new List<Claim>();
                    var roles = await _userManager.GetRolesAsync(user);
                    roles.ToList().ForEach(i =>
                    {
                        claims.Add(new Claim(ClaimTypes.Role, i));
                    });

                    JwtSecurityToken token
                        = new JwtSecurityToken
                        (
                            signingCredentials:
                             new SigningCredentials
                             (
                                 new SymmetricSecurityKey(Encoding.ASCII.GetBytes("IOLJYHSDSIoleJHsdsdsas98WeWsdsdQweweHgsgdf_&6#2"))
                                 ,
                                 SecurityAlgorithms.HmacSha256
                             ),
                            expires: DateTime.Now.AddDays(5),
                            claims: claims
                        );

                    string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                    myModel.Success = true;
                    myModel.Message = "Successfulyy Loged In";
                    myModel.Data = new
                    {
                        User = user,
                        Toekn = tokenValue,
                        Roles = roles
                    };
                }
            }
            return myModel;
        }
        //[HttpPost("login")]
        //public async Task<IActionResult> Login([FromBody] LoginUserDto request)
        //{
        //    var user = await _userManager.FindByNameAsync(request.Email);
        //    if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        //    {
        //        return Unauthorized();
        //    }

        //    var roles = await _userManager.GetRolesAsync(user);

        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new[]
        //        {
        //    new Claim(ClaimTypes.NameIdentifier, user.Id),
        //    new Claim(ClaimTypes.Name, user.UserName),
        //    new Claim(ClaimTypes.Role, string.Join(",", roles)),
        //}),
        //        Expires = DateTime.UtcNow.AddHours(_appSettings.TokenExpirationHours),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);

        //    return Ok(new { token = tokenHandler.WriteToken(token) });
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// 


        //[HttpPost]
        //public async Task<ResultModel> LogIn([FromBody] LoginUserDto loginUserDto)
        //{
        //    var myModel = new ResultModel();

        //    _logger.LogInformation($"LogIn Attempt For {loginUserDto.Email}");
        //    if (!ModelState.IsValid)
        //    {
        //        myModel.Success = false;
        //        myModel.Data =
        //            ModelState.Values.SelectMany
        //                    (i => i.Errors.Select(x => x.ErrorMessage));

        //        //return BadRequest(ModelState);
        //    }
        //    try
        //    {

        //        var result = await _signInManager
        //            .PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, false, false);
        //        if (!result.Succeeded)

        //        {
        //            return BadRequest("error");
        //        }
        //        else
        //        {
        //            var User = await _userManager.FindByEmailAsync(loginUserDto.Email);
        //            List<Claim> claims = new List<Claim>();
        //            var roles = await _userManager.GetRolesAsync(User);
        //            roles.ToList().ForEach(i =>
        //            {
        //                claims.Add(new Claim(ClaimTypes.Role, i));
        //            });

        //            JwtSecurityToken token
        //                = new JwtSecurityToken
        //                (
        //                    signingCredentials:
        //                     new SigningCredentials
        //                     (
        //                         new SymmetricSecurityKey(Encoding.ASCII.GetBytes("IOLJYHSDSIoleJHsdsdsas98WeWsdsdQweweHgsgdf_&6#2"))
        //                         ,
        //                         SecurityAlgorithms.HmacSha256
        //                     ),
        //                    expires: DateTime.Now.AddDays(5),
        //                    claims: claims
        //                );

        //            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        //            myModel.Success = true;
        //            myModel.Message = "Successfulyy Loged In";
        //            myModel.Data = new
        //            {
        //                User = User,
        //                Toekn = tokenValue,
        //                Roles = roles
        //            };


        //            return Accepted();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        {
        //            _logger.LogError(ex, $"somethoing went wrong in the {nameof(LogIn)}");
        //            return Problem($"somethoing went wrong in the {nameof(LogIn)}", statusCode: 500);

        //        }

        //    }

        //}

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            
            return Ok();
        }
        //[HttpGet]
        //public async Task<IActionResult> getUserId(string email)
        //{

        //    var User = await _userManager.FindByEmailAsync(email);

        //    if (User != null)
        //    {
        //        return Ok(User.Id);

        //    }
        //    else
        //    {
        //        return BadRequest("error");
        //    }

        //User user = await _context.Users.SingleAsync(u => u.Email == email);
        //if (user != null)
        //{
        //    long id = user.Id;
        //    //return Ok( _context.Users.SingleAsync(u => u.Email == email).Id);
        //    return id;
        //}

        //return 0;
        //}

        [HttpPatch]
        public async Task<IActionResult> updateUser([FromBody] checkoutDto checkoutDto)//long id,string address,string phone)


        {
           
           // var cookie = Request.Cookies["MyCookie"];
           //var cookie = Request.Cookies["userid"];
           // var cookies = HttpContext.Request.Cookies;
           // var name = HttpContext.Request.Cookies["MyCookie"];
           // if (cookie != null)
           // {
               // var id = long.Parse(cookie);
             //   var user = await _userManager.FindByIdAsync(id.ToString());
                var user = await _userManager.FindByEmailAsync(checkoutDto.useremail);

                if (user != null)
                {
                    user.Address = checkoutDto.Adress;
                    user.Phone = checkoutDto.Phone;
                _context.Update(user);
               await _context.SaveChangesAsync();
                    return Ok(user);
                }
                //else
                //    return BadRequest("error");
          //  }
            else
                return BadRequest("error");
        }

        [HttpGet]
        public async Task<IActionResult> getnameuser(string useremail)
        {
            var user = await _userManager.FindByEmailAsync(useremail);
            if (user != null)
            {
                string fullname = user.FirstName + " " + user.LastName;
                return Ok(fullname);
            }
            else
                return BadRequest("error");
        }

    }

}
