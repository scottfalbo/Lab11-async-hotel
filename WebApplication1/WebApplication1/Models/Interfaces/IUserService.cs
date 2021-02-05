using AsyncHotel.Models.Api;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> Register(RegisterUser data, ModelStateDictionary modelState);

        Task<UserDto> Authenticate(string userName, string password);

        Task<UserDto> GetUser(ClaimsPrincipal user);
    }
}
