using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweeterBook.Contract;
using AutoMapper;
using Microsoft.OpenApi.Models;
using TweeterBook.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;
using TweeterBook.Repository;
using TweeterBook.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using TweeterBook.Filter;

namespace TweeterBook.Installers
{
    public class ApiInstaller : IInstallers
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = new JwtSettings();
            configuration.Bind(nameof(JwtSettings), jwtSettings);
            services.AddSingleton(jwtSettings);

            services.AddMvc(options => { options.EnableEndpointRouting = false; options.Filters.Add<ValidationFilter>(); })
                .AddFluentValidation(mvcConifg => mvcConifg.RegisterValidatorsFromAssemblyContaining<Startup>())
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddScoped<IIdentityRepository, IdentityRepository>();

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true
            };

            services.AddSingleton(tokenValidationParameters);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme; ;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters;

                });

            services.AddAuthorization(option =>
            {
                // option.AddPolicy("Tagviewer", builder => builder.RequireClaim("tags.view", "true"));

                 option.AddPolicy("MustworkwithDomain", policy =>
                 {
                     policy.AddRequirements(new WorksForCompanyRequirement("@localhost"));
                 });

            });

            services.AddSingleton<IAuthorizationHandler, WorksForCompanyHandler>();           


            services.AddSingleton<IUriService>(provider =>
            {
                var accessor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accessor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent(), "/");
                return new UriService(absoluteUri);
            });


        }
    }
}
