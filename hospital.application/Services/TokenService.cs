using hospital.application.Interfaces;
using hospital.domain.People;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace hospital.application.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        #region //Constructor
        public TokenService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        #endregion


        public (string Token, DateTime ExpiresAt) GenerateToken(UserAccount account)
        {
            var jwtKey = configuration["Jwt:Key"]
                ?? throw new InvalidOperationException("JWT key is not configured.");

            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audience"];
            var expiresInMinutes = int.Parse(configuration["Jwt:ExpiresInMinutes"] ?? "60");


            var expiresAt = DateTime.UtcNow.AddMinutes(expiresInMinutes);

            var claim = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new(ClaimTypes.Email, account.Email),
                new(ClaimTypes.Role, account.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claim,
                expires: expiresAt,
                signingCredentials: credentials
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return (tokenString, expiresAt);
        }
    }
}
