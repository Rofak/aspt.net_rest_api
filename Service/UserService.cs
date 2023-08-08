using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Restfull.Data;
using Restfull.DTO;
using Restfull.Model;
using Restfull.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Restfull.Service
{
    public class UserService:CrudService<User>
    {

       public DataContext Context;
       private IConfiguration _configuration;

       public UserService(DataContext context, IConfiguration configuration) : base(context)
        {
            Context = context;
            _configuration = configuration;
        }


        public async Task<String> Login(LoginDto loginDto)
        {

            if(loginDto.password!=null && loginDto.email!=null) { 
                var isAuthunticatedUser=await veriyUserLogin(loginDto);
                if(isAuthunticatedUser!=null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Email", isAuthunticatedUser.email),
                        new Claim("Name",isAuthunticatedUser.name),
                        
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
                    var singIn=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["JWT:Issuer"],
                        _configuration["JWT:Audience"],
                        claims,
                        expires:DateTime.UtcNow.AddDays(1),
                        signingCredentials: singIn);
                    var dataToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return dataToken;
                }
            }

            return "Invalid Credentials";
        }

        public async Task<User> veriyUserLogin(LoginDto loginDto) {

           var user = await table.FirstOrDefaultAsync(
                user => user.email == loginDto.email);
            if (user != null && BCrypt.Net.BCrypt.Verify(loginDto.password,user.password))
            {
                return user;
            }
            return null;
        }

    }
}
