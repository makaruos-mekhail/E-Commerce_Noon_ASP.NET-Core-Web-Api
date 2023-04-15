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
                    var loginuser = new LoginUserDto()
                    {
                        Email = userdto.Email,
                        Password = userdto.Password
                    };
                    //return RedirectToAction("SignIn",loginuser);
                   
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
       

        [HttpPost]
        public async Task<IActionResult> LogOut()
        {

            await _signInManager.SignOutAsync();
            
            return Ok();
        }
       
        [HttpPatch]
        public async Task<IActionResult> updateUser([FromBody] checkoutDto checkoutDto)//long id,string address,string phone)


        {
           
       
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
                string fullname = user.FirstName ;
                return Ok(fullname);
            }
            else
                return BadRequest("error");
        }

    }

}
