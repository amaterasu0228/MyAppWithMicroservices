using Microsoft.AspNetCore.Mvc;
using AuthService.API.Models;
using AuthService.API.Services;

namespace AuthService.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Login and Password required");

            bool success = _authService.Authenticate(request.Login, request.Password);

            if (success)
                return Ok();
            else
                return Unauthorized();
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            _authService.Logout();
            return Ok();
        }
    }
}
