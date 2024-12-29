using iCare4H.Service.Domain.Interface;
using iCare4H.Service.Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace iCare4H.Service.Application
{
    public class UserService(IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository userRepository = userRepository;

        public string Authenticate(string username, string password)
        {
            var isValid = userRepository.ValidateUser(username, password);
            if (isValid)
            {
                // authentication successful so generate jwt token
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("SomeSecret to be added to config file..111@@##$$56");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("username", username.ToString()),
                        // new Claim("currency", user.Currency),
                        // new Claim("name", user.FullName)
                    }),
                    Expires = DateTime.UtcNow.AddDays(7),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtSecurityToken = tokenHandler.WriteToken(token);

                return jwtSecurityToken;
            }
            return string.Empty;
        }
    }
}
