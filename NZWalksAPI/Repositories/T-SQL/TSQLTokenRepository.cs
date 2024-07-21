using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NZWalks.API.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NZWalksAPI.Repositories.T_SQL
{
    public class TSQLTokenRepository : ITokenRepository
    {
        private readonly IConfiguration _configuration;

        public TSQLTokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(IdentityUser user, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(45),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
