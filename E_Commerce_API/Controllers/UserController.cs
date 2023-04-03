using AutoMapper;
using Context;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Logging;
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

        public UserController(DContext context,
            UserManager<User> userManager
            , SignInManager<User> signInManager,
            ILogger<UserController> logger,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;

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
                await _userManager.AddToRoleAsync(user, "Customer");


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
                return Accepted();

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
    
    }

}
