using EmployeeManagement.WebApi.Common;
using EmployeeManagement.WebApi.IServices;
using EmployeeManagement.WebApi.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagement.WebApi.Services
{
    public class IdentityService : IIdentityService
    {
      
       
        private List<RegisterModel> _users = new List<RegisterModel>
        {
            new RegisterModel { Id = 1, FirstName = "Admin", LastName = "User", Username = "admin", Password = "admin", Role = Role.Admin },
            new RegisterModel { Id = 2, FirstName = "Normal", LastName = "User", Username = "user", Password = "user", Role = Role.User }
        };
        private readonly AppSettings _appSettings;
        public IdentityService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public  AuthenticationResult LoginAsync(LoginModel model)
        {
            var user = _users.SingleOrDefault(x => x.Username == model.UserName && x.Password == model.Password);
            if (user == null)
                return new AuthenticationResult
                {
                    Errors = new[] { "User does not exist." }
                };

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);
            return new AuthenticationResult
            {
                Success = true,
                Token = tokenHandler.WriteToken(token),
             
            };

        }

    

      
    }
}
