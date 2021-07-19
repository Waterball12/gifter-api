using Gifter.Domain.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Gifter.Infrastructure
{
    public class TokenUtils
    {
        public static string GetToken(List<Claim> claims, TokenOptions options)
        {
            var issuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Secret));

            var creds = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(options.Issuer, options.Audience, claims, expires: DateTime.UtcNow.AddDays(30), signingCredentials: creds);

            var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            return tokenValue;
        }
    }
}
