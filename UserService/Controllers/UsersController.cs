using Microsoft.AspNetCore.Mvc;
using UserService.API.Services;
using UserService.API.Models;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Login) || string.IsNullOrWhiteSpace(request.Password))
                return BadRequest("Необхідно заповнити логін та пароль");

            bool success = _userService.Register(request.Username, request.Phone, request.Login, request.Password);

            if (success)
                return Ok();
            else
                return Conflict("Користувач з таким логіном вже існує");
        }
    }
}
