using Data.SQLServer.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer
{
    public static class DependencyInjection
    {
        public static IServiceCollection GetDataServices(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>();
            services.AddDbContext<IdentityAppContext>();
            var builder = services.AddIdentityCore<UserApp>();
            builder = new IdentityBuilder(builder.UserType, builder.Services);
            builder.AddEntityFrameworkStores<IdentityAppContext>();
            builder.AddSignInManager<SignInManager<UserApp>>();
            services.AddAuthentication();
            services.AddTransient<IDbResult, DbResults>();

            return services; 
        }
    }
}
