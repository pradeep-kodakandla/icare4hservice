﻿using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Domain.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iCare4H.Service.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Login loginParam)
        {
            if (loginParam == null || string.IsNullOrEmpty(loginParam.UserName) || string.IsNullOrEmpty(loginParam.Password))
            {
                return BadRequest(new { error = "Invalid request payload" });
            }

            var user = _userService.Authenticate(loginParam.UserName, loginParam.Password);

            if (user == null)
            {
                return Unauthorized(new { error = "Username or password is incorrect" });
            }

            // 🔹 Generate JWT Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.Role, "User")
        }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // ✅ Ensure JSON response and correct Content-Type
            var response = new
            {
                Token = tokenString,
                UserName = user.UserName,
                Message = "Login successful!"
            };

            return new JsonResult(response) { ContentType = "application/json" };
        }

        // ✅ Handle CORS Preflight for Angular
        [HttpOptions("authenticate")]
        public IActionResult Preflight()
        {
            Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
            Response.Headers.Add("Access-Control-Allow-Headers", "Content-Type, Authorization");
            Response.Headers.Add("Access-Control-Allow-Methods", "POST, OPTIONS");
            return Ok();
        }
    }
}
