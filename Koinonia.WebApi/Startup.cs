using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Koinonia.Application.HelperClass;
using Koinonia.Domain.Models;
using Koinonia.Infra.Data.Context;
using Koinonia.Infra.Ioc;
using Koinonia.WebApi.Data;
using Koinonia.WebApi.Helpers;
using Koinonia.WebApi.Implementation;
using Koinonia.WebApi.Interface;
using Koinonia.WebApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Koinonia.WebApi
{
    public class Startup
    {
        private const string SECRET_KEY = "TQvgjeABMPOwCyOqah5EQU5yyVjpmVGTQvgjeABMPOwCycOqah5EQu%yyVjpmVGTQv";
        public static readonly SymmetricSecurityKey SIGNING_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SECRET_KEY));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddDbContext<KoinoniaDbContext>(opt =>
            {
                opt.UseSqlServer(Configuration.GetConnectionString("KoinoniaConnection"));
            });
            services.AddControllers();

            services.AddSwaggerGen(sw => {
                sw.SwaggerDoc("v1", new OpenApiInfo { Title = "Koinonia API", Version = "v1" });
            });

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            
            RegisterServices(services);
            services.AddScoped<IAuthentication, Authentication>(); //used to authenticate the jwt token
        }

        private static void RegisterServices(IServiceCollection services)
        {
            DependencyContainer.RegisterServices(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(sw =>
            {
                sw.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseRouting();

            app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

            //custom jwt middleware
            app.UseMiddleware<JwtMiddleware>();

            app.UseAuthorization();

            //seed the required roles
            //SeedRoles.SeedData(roleManager);

            //seed the required users
            //SeedUsers.SeedData(userManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
