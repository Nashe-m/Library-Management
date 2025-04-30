using LibraryManagement.AuthDto;
using LibraryManagement.Models;
using LibraryManagement.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
        public class AccountController(IAuthService authService) : ControllerBase
        {

            [HttpPost("register")]
            public async Task<ActionResult<User>> Register(UserDto request)
            {
                var user = await authService.RegisterAsync(request);
                if (user == null) return BadRequest("Username Already Exists");
                return Ok(user);
            }
            [HttpPost("login")]
            public async Task<ActionResult<TokenResponseDto>> Login(UserDto request)
            {
                var response = await authService.LogInAsync(request);
                if (response == null) return BadRequest("Invalid details");
                return Ok(response);
            }
            [HttpPost("refresh-token")]
            public async Task<ActionResult<TokenResponseDto>> RefreshToken(RefreshTokenRequestDto request)
            {
                var result = await authService.RefreshTokenAsync(request);
                if (result == null || result.AccessToken == null || result.RefreshToken == null) return Unauthorized("Invalid Refresh Token");
                return Ok(result);
            }
        }
    
}
