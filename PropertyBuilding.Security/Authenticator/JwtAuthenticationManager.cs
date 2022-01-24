using Microsoft.IdentityModel.Tokens;
using PropertyBuilding.Security.Interfaces;
using PropertyBuilding.Transversal.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PropertyBuilding.Security.Authenticator
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
      
            private readonly IDictionary<string, string> users = new Dictionary<string, string>
        { {"user1","pass1"},{"user2","pass2"}};

        private readonly string key;
        public JwtAuthenticationManager(string key)
        {
            this.key = key;
        }
        public string Authenticate(User credentials)
        {
            if (!users.Any(u => u.Key == credentials.Name && u.Value == credentials.Password))
            {
                return null;
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.Name, credentials.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }




    }
    
}
