using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restfull.Data;
using Restfull.DTO;
using Restfull.Lib;
using Restfull.Model;
using Restfull.Service;

namespace Restfull.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : CrudController<User,UserService>
    {

        public UserController(DataContext dataContext,IConfiguration configuration):
            base(new UserService(dataContext,configuration))
        {
   
        }

        public override async Task<User> Create([FromBody] User entity)
        {
            entity.password= BCrypt.Net.BCrypt.HashPassword(entity.password,workFactor:13);
            return await service.Create(entity);
        }

        public override async Task<User> Update(int id, User entity)
        {
            entity.password = BCrypt.Net.BCrypt.HashPassword(entity.password, workFactor: 13);
            return await service.Update(id, entity);
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<String> Login([FromBody] LoginDto loginDto)
        {
            return await service.Login(loginDto);
        }

    }
}
