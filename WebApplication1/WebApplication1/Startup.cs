using AsyncHotel.Data;
using AsyncHotel.Models;
using AsyncHotel.Models.Interfaces;
using AsyncHotel.Models.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Set up a context to our database for us in the App.
            services.AddDbContext<AsyncDbContext>(options => {
                string connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<AsyncDbContext>();

            services.AddScoped<JwtTokenService>();

            // This will allow us to decorate routes (annotate)
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = JwtTokenService.GetValidation(Configuration);
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("create", policy => policy.RequireClaim("permission", "create"));
                options.AddPolicy("update", policy => policy.RequireClaim("permission", "update"));
                options.AddPolicy("delete", policy => policy.RequireClaim("permission", "delete"));
            });

            services.AddTransient<IRoom, RoomRepository>();
            services.AddTransient<IHotel, HotelRepository>();
            services.AddTransient<IAmenities, AmenitiesRepository>();
            services.AddTransient<IHotelRoom, HotelRoomRepository>();
            services.AddTransient<IUserService, IdentityUserService>();

            services.AddMvc();
            services.AddControllers();

            // Newtonsoft to prevent infinite reference looping
            services.AddControllers().AddNewtonsoftJson(options =>
              options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddSwaggerGen(options =>
            {
                // Make sure get the "using Statement"
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = "Async Hotel",
                    Version = "v1",
                });
            });








            // TODO: in class, register service




        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //TODO: todays stuff

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(options =>
            {
                options.RouteTemplate = "/api/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/api/v1/swagger.json", "Async Hotel");
                options.RoutePrefix = ""; //if we want a real homepage this is "docs"
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
