using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace WebApplicationViajeCero.Models
{
    public class TokenJWT
    {
        private readonly IConfiguration _config;

        public TokenJWT(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateToken(string Identification, string role)
        {
            var claims = new[]
            {
                new Claim (ClaimTypes.Name, Identification),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(1),
                    signingCredentials: creds

                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

    }
}
