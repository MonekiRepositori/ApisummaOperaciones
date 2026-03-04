using ApiGruposummaOperaciones.Data;
using ApiGruposummaOperaciones.Models;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ApiGruposummaOperaciones.Security
{
    public class JwtServices
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;

        public JwtServices(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public string GenerateToken(int userId, string userRole)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                new Claim(ClaimTypes.Role, userRole),
               
                
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiresInMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        //Method to generate a refresh token.
        public RefreshToken GenerateRefreshTocken()
        {
            var refreshTocken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                CreationDate = DateTime.UtcNow,
                DateTimeExpirationDate = DateTime.UtcNow.AddDays(7)
            };
            return refreshTocken;
        }
    }
}