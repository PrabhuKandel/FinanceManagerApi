using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using FinanceManager.Application.Interfaces.Services;
using FinanceManager.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace FinanceManager.Application.Services
{
    public class TokenGenerator:ITokenGenerator
    {
        private readonly IConfiguration _config;

        public TokenGenerator(IConfiguration config)
        {
            _config = config;
        }
        public string GenerateAccessToken(ApplicationUser user)
        {

            var claims = new List<Claim>
            {
                new Claim("email",user.Email),
                new Claim("userId", user.Id),

            };
            var SignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:SecretKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(SignKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                signingCredentials: signingCredentials
                );
            string tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenString;

        }

        public string GenerateRefreshToken()
        {

            return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));


        }
    }
}
