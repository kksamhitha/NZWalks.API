using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager , ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository; 
        }
        [HttpPost]
        [Route("Register")]
        //Post:/api/Auth/Register
        public async Task<IActionResult> Register([FromBody]RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            { 
                UserName = registerRequestDto.userName,
                Email = registerRequestDto.userName
            };
           var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.password);
            if (identityResult.Succeeded)
            {
                //Add roles to the user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any()){
                   identityResult =  await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if(identityResult.Succeeded)
                    {
                        return Ok("User was registered. Please login");
                    }
                }
            }
            return BadRequest("Something is wrong.");
        } 

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var user = await userManager.FindByEmailAsync(loginRequestDto.userName);

            if(user != null)
            {
                var checkPasswordResult =  await userManager.CheckPasswordAsync(user, loginRequestDto.password);
                if(checkPasswordResult)
                {
                    //Get Roles for the user
                   var roles =  await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        //Create Token
                        var jwtToken =  tokenRepository.CreateJWTToken(user, roles.ToList());
                        var response = new LoginResponseDto
                        {
                            jwtToken = jwtToken
                        };
                        return Ok(response);    
                    }
                    return Ok();
                }
            }
            return BadRequest("Username and Password combination is incorrect");    
        }
    }
}
