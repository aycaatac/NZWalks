using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTOs;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepoistory;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepoistory)
        {
            this.userManager = userManager;
            this.tokenRepoistory = tokenRepoistory;
        }

        //post because we are creating information
        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] InputRegisterDto registerDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Username
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerDto.Password);

            if (identityResult.Succeeded)
            {
                //add roles to this user if the user is created
                if(registerDto.Roles != null && registerDto.Roles.Any())
                {
                   identityResult = await userManager.AddToRolesAsync(identityUser, registerDto.Roles);

                    if (identityResult.Succeeded)
                    {
                        return Ok("User was registered! Please login.");
                    }
                }

            }

            return BadRequest("Something went wrong! Please try again!");
        }


        //POST: /api/Auth/Login
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await userManager.FindByEmailAsync(loginDto.Username);
            if(user != null)
            {
               var passwordValid = await userManager.CheckPasswordAsync(user, loginDto.Password);
                if (passwordValid)
                {
                    var roles = await userManager.GetRolesAsync(user);

                    if(roles != null)
                    {
                        //create token and return it

                        //create token in repository
                        
                        var token = tokenRepoistory.CreateJWTToken(user, roles.ToList());

                        var response = new ReturnLoginDto
                        {
                            JwtToken = token,
                            EmailAddress = user.Email
                        };

                        return Ok(response);
                    }
                    
                }
            }

            return BadRequest("Username or password is incorrect! Please try again!");
        }
    }
}
