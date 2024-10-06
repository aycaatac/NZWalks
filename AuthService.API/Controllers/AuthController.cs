using AuthService.API.Models.DTO;
using AuthService.API.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;        

        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            //password uymasa bile registered diyor!!!!!!!!!!!!!!!!!!!!
            var user = await authService.Register(registerUserDto);
            if(!string.IsNullOrEmpty(user))
            {
                return BadRequest(user);
            }
          
            return Ok("User was registered! Please login!");
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var response = await authService.Login(loginUserDto);
            if(response == null)
            {
                return BadRequest("Username or password is invalid! Please try again!");
            }

            return Ok(response);
        }

        [HttpPost]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterUserDto userDto)
        {            
            var response = await authService.AssignRole(userDto.Email.ToLower(), userDto.Role.ToLower());
            if(response == false)
            {
                return BadRequest("The role was NOT assigned to the user!");
            }

            return Ok("The role was assigned to the user!");
        }
    }
}
