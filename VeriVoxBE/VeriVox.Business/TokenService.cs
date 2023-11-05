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
using VeriVox.Database.DatabaseObjects;
using VeriVox.Repository.Interfaces;

namespace VeriVox.Business
{
    public class TokenService
    {
        private readonly IConfiguration? _configuration;
        private readonly IUserRepository _userRepository;
        public TokenService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }

        public string GenerateJwtToken(User user)
        {

            string? secretKey = _configuration?["AppSettings:Passphrase"];

            var issuer = _configuration?["AppSettings:IssuerLink"];
            var audience = "http://localhost:3000/";

            var expirationTime = DateTime.UtcNow.AddMinutes(59); // time after which the token would expire
            var userRole = _userRepository.GetUserRoleById(user.Id);

            var claims = new[]
            {
               new Claim("Id",user.Id.ToString()),
               new Claim("FirstName",user.FirstName),
               new Claim("LastName",user.LastName),
               new Claim("Email",user.EmailId),
               new Claim("Role", userRole.Result.RoleId.ToString())
            };
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: expirationTime,
            notBefore: DateTime.UtcNow,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512)
            );

            // Serialize the token to a string
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }
    }
}