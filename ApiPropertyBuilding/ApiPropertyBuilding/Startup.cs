using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PropertyBuilding.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PropertyBuilding.Domain.IRepositories;
using PropertyBuilding.Domain.IUnitOfWorks;
using PropertyBuilding.Infrastructure.UnitOfWorks;
using PropertyBuilding.Security.Interfaces;
using PropertyBuilding.Security.Authenticator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ApiPropertyBuilding
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opst => opst.UseSqlServer(Configuration.GetConnectionString( "database")));
            services.AddScoped<DatabaseContext>();
            services.AddScoped<IPropertyRepository, PropertyRepository>();
            services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));

            //JWT
            var key = "this is a key test for authorize access to controllers";
            services.AddAuthentication(x => {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddSingleton<IJwtAuthenticationManager>(new JwtAuthenticationManager(key));
            //swagger
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("V1",
                    new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Test For Developer .Net",
                        Description = "Api Created for obtain information about properties in the US",
                        Version = "V1"
                    }
                    );
            });
          
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options => {
                options.SwaggerEndpoint("../swagger/V1/swagger.json", "Test For Developer .Net");
            });
            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
          
        }
    }
}
