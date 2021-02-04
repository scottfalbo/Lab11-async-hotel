using AsyncHotel.Models.Api;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces.Services
{
    public class IdentityUserService : IUserService
    {
        private UserManager<AppUser> userManager;

        public IdentityUserService(UserManager<AppUser> manager)
        {
            userManager = manager;
        }

        public async Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState)
        {
            var user = new AppUser()
            {
                UserName = data.UserName,
                Email = data.Email,
                PhoneNumber = data.PhoneNumber
            };
            var result = await userManager.CreateAsync(user, data.Password);

            if (result.Succeeded)
            {
                return new UserDto 
                { 
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            //put errors into modelState, changes state in controller as well
            foreach (var error in result.Errors)
            {
                var errorKey =
                    error.Code.Contains("Password") ? nameof(data.Password) :
                    error.Code.Contains("Email") ? nameof(data.Email) :
                    error.Code.Contains("UserName") ? nameof(data.UserName) :
                    "";

                modelState.AddModelError(errorKey, error.Description);
            }

            return null;
        }

        public async Task<UserDto> Authenticate(string userName, string password)
        {
            var user = await userManager.FindByNameAsync(userName);

            if (await userManager.CheckPasswordAsync(user, password))
            {

                return new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName
                };
            }

            return null;
        }
    }
}
