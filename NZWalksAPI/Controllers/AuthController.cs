using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.Models.DTO.Auth;
using NZWalks.API.Repositories;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.UserName,
                Email = registerRequestDto.UserName
            };

            var identityRes = await _userManager.CreateAsync(identityUser, registerRequestDto.Password);
            if (identityRes.Succeeded)
            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityRes = await _userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityRes.Succeeded)
                    {
                        return Ok("User Registered! Please log in.");
                    }
                }
            }

            return BadRequest("Something went wrong during registration");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var user = await _userManager.FindByEmailAsync(loginRequestDto.UserName);
            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (result)
                {
                    // Get user roles
                    var roles = await _userManager.GetRolesAsync(user);
                    if (roles != null)
                    {
                        var response = new LoginResponseDto
                        {
                            JwtToken = _tokenRepository.CreateToken(user, roles.ToList())
                        };

                        return Ok(response);
                    }
                }

                return BadRequest("Password is incorrect!");
            }

            return BadRequest("UserName is incorrect!");
        }
    }
}
