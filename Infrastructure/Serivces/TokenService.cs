using Core.entites.Identity;
using Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Serivces
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey key;

        public TokenService(IConfiguration _configuration) 
        {
            configuration = _configuration;
            key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:Key"]));

        }
        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email,user.Email ),
                new Claim(ClaimTypes.GivenName,user.DisplayName ),
            };

            var creditials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescreptor = new SecurityTokenDescriptor()
            {
                SigningCredentials = creditials,
                Issuer = configuration["Token:Issuer"],
                //IssuedAt = DateTime.Now,
                Expires = DateTime.Now.AddDays(7),
                Subject = new ClaimsIdentity(claims),
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token =tokenHandler.CreateToken(tokenDescreptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
