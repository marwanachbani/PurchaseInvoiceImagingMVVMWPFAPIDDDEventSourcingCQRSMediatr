using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Infra.Indentity
{
    public static class AddIdentity
    {
        public static IServiceCollection GetServices(this IServiceCollection services)
        {
            services.AddIdentityCore<UserApp>(opt =>
            {
                // add identity options here
            })
            .AddEntityFrameworkStores<AppIdentityDbContext>()
            .AddSignInManager<SignInManager<UserApp>>();
            return services; 
        }
    }
}
