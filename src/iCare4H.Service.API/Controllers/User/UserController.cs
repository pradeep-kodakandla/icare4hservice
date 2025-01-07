using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iCare4H.Service.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Login loginParam)
        {
            var token = _userService.Authenticate(loginParam.UserName, loginParam.Password);

            if (token == null || token == string.Empty)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(token);
        }

    }
}