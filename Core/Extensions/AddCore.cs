using Core.EventSourcing;
using Core.Exceptions;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Extensions
{
    public static class AddCore
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IEventStoreServices, EventStoreServices>();
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(AddCore).Assembly));
            services.AddTransient(typeof(IMediatrException<,,>),typeof(BaseCommandRequestHandlerException<,,>));
            
            return services; 
        }
    }
}
