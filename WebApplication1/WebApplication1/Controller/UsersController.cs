using AsyncHotel.Models;
using AsyncHotel.Models.Api;
using AsyncHotel.Models.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService service)
        {
            userService = service;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<UserDto>> Register(RegisterUser data)
        {
            var user = await userService.Register(data, this.ModelState);

            if(ModelState.IsValid)
                return user;

            return BadRequest(new ValidationProblemDetails(ModelState));
        }

        [HttpPost("Login")]
        public async Task<ActionResult<UserDto>> Login(LoginData data)
        {
            var user = await userService.Authenticate(data.UserName, data.Password);
            if (user != null)
                return user;
            return Unauthorized();
        }
    }
}
