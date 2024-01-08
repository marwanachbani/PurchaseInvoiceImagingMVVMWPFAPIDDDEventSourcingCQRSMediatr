using ImageStore.Bridges;
using ImageStore.Commands;
using ImageStore.Handlers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore.Extensions
{
    public static class AddImageStore 
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IImageStoreBridge, ImageStoreBridge>();
    
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(AddImageStore).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(AddImage).Assembly,
                typeof(AddImageHandler).Assembly));
            return services;
        }
    }
}
