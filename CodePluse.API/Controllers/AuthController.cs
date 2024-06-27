using CodePluse.API.Model.Dto;
using CodePluse.API.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CodePluse.API.Controllers
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
        [HttpPost("/Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto requestDto)
        {
            var identityUser = await _userManager.FindByEmailAsync(requestDto.Email);
            if (identityUser is not null)
            {
                var isCorrect = await _userManager.CheckPasswordAsync(identityUser, requestDto.Password);
                if (isCorrect)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);
                    //create token 
                    var loginResponse = new LoginResponseDto()
                    {
                        Email = requestDto.Email,
                        Roles = roles.ToList(),
                        Token = _tokenRepository.CreateToken(identityUser, roles.ToList())
                    };
                    return Ok(loginResponse);
                }
            }
            ModelState.AddModelError("", "Email or Password Incorrect");
            return ValidationProblem(ModelState);
        }
        [HttpPost("/Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto requestDto)
        {
            var user = new IdentityUser()
            {
                Email = requestDto.Email?.Trim(),
                UserName = requestDto.Email?.Trim()
            };
            var IdentityResult = await _userManager.CreateAsync(user, requestDto.Password);
            if (IdentityResult.Succeeded)
            {
                IdentityResult = await _userManager.AddToRoleAsync(user, "Reader");
                if (IdentityResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    if (IdentityResult.Errors.Any())
                    {
                        foreach (var error in IdentityResult.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
            }
            else
            {
                if (IdentityResult.Errors.Any())
                {
                    foreach (var error in IdentityResult.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return ValidationProblem(ModelState);
        }
    }
}
