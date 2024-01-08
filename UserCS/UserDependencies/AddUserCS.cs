using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserCS.Commands;
using UserCS.Handlers;
using Core.Extensions;
using UserCS.Bridge;
using Microsoft.AspNetCore.Identity;
using Data.SQLServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MediatR;
using UserCS.Services;
using Microsoft.Extensions.Configuration;

namespace UserCS.UserDependencies
{
    public static class AddUserCS
    {
        public static IServiceCollection GetUserServices(this IServiceCollection services,IConfiguration config)
        {
            AddCore.AddServices(services);
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(AddUserCS).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(Connect).Assembly,
                typeof(ConnectHandler).Assembly));
            services.AddTransient<IUserCommandBridge, UserCommandBridge>();
            services.AddTransient<ITokenServices, TokenServices>();
            services.AddIdentityCore<UserApp>(opt =>
            {
                // add identity options here
            })
            .AddEntityFrameworkStores<IdentityAppContext>()
            .AddSignInManager<SignInManager<UserApp>>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuerSigningKey = true,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("kan2anak bitb3 el3inad sindibad ou 3achiq ou 3ch9ak b3id elbilad")),
                       
                       ValidateIssuer = true,
                       ValidateAudience = false
                   };
                   options.TokenValidationParameters.NameClaimType = "name";
                   options.TokenValidationParameters.ValidateIssuer = false;
               });
        


            services.AddAuthorization();
            
            return services; 
        }
    }
}
