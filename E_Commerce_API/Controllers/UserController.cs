using AutoMapper;
using Azure;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using Repository.DTOs;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Threading;

namespace E_Commerce_API.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DContext _context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        private readonly HttpContext _httpContext;

        public UserController(DContext context,
            UserManager<User> userManager
            , SignInManager<User> signInManager,
            ILogger<UserController> logger,
            IMapper mapper,
            IHttpContextAccessor httpContext)
        {
            _context = context;
            _userManager = userManager;
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
                await _userManager.AddToRoleAsync(user, "TEST");


                if (!result.Succeeded)
                {

                    return BadRequest("error");
                }

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"somethoing went wrong in the {nameof(Registeration)}");
                return Problem($"somethoing went wrong in the {nameof(Registeration)}", statusCode: 500);
            }
           

        }
        [HttpPost]
        public async Task<IActionResult> LogIn([FromBody] LoginUserDto loginUserDto)
        {
            _logger.LogInformation($"LogIn Attempt For {loginUserDto.Email}");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                var result = await _signInManager
                    .PasswordSignInAsync(loginUserDto.Email, loginUserDto.Password, false, false);
                if (!result.Succeeded)

                { 
                    return BadRequest("error");
                }
                else 
                {
                    var User = await _userManager.FindByEmailAsync(loginUserDto.Email);

                    if (User!=null)
                    {
                        //var cookieOptions = new CookieOptions();
                        //Response.Cookies.Append("userId", User.Id.ToString(), cookieOptions);
                        var cookieOptions = new CookieOptions();
                        cookieOptions.Expires = DateTime.Now.AddDays(30);
                        cookieOptions.Path = "/";
                        Response.Cookies.Append("userId", User.Id.ToString(), cookieOptions);

                    }

                    return Accepted();
                }
                
            }
            catch (Exception ex)
            {
                {
                    _logger.LogError(ex, $"somethoing went wrong in the {nameof(LogIn)}");
                    return Problem($"somethoing went wrong in the {nameof(LogIn)}", statusCode: 500);

                }

            }

        }

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
    }

}
