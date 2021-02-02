using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Salary.Core.Common;
using Salary.Core.Configurations;
using Salary.Core.Entities.Identity;
using Salary.Core.Interfaces;

namespace Salary.Business.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtTokenConfig _tokenConfig;

        public JwtService(IOptions<JwtTokenConfig> options)
        {
            _tokenConfig = options.Value;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim("Id", user.Id.ToString()),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email)
            };

            var credentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Secret)),
                    SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials,
                Audience = _tokenConfig.Audience,
                Issuer = _tokenConfig.Issuer
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}