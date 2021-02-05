using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncHotel.Models.Interfaces.Services
{
    public class JwtTokenService
    {
        private readonly IConfiguration configuration;
        private readonly SignInManager<AppUser> signInManager;

        public JwtTokenService(IConfiguration config, SignInManager<AppUser> manager)
        {
            configuration = config;
            signInManager = manager;
        }

        /// <summary>
        /// validates secrets
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public static TokenValidationParameters GetValidation(IConfiguration config)
        {
            return new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = GetSecurityKey(config),
                ValidateIssuer = false,
                ValidateAudience = false,
            };
        }
        public static SecurityKey GetSecurityKey(IConfiguration config)
        {
            var secret = config["JWT:Secret"];
            if (secret == null)
                throw new InvalidOperationException("no secret");

            var secretBytes = Encoding.UTF8.GetBytes(secret);
            return new SymmetricSecurityKey(secretBytes);
        }

        /// <summary>
        /// Create a token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public async Task<string> GetToken(AppUser user, System.TimeSpan expiresIn)
        {
            var principal = await signInManager.CreateUserPrincipalAsync(user);

            if (principal == null)
                return null;

            var signingKey = GetSecurityKey(configuration);
            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow + expiresIn,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256),
                claims: principal.Claims
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
