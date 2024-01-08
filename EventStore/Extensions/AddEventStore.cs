using Core;

using Core.Extensions;

using EventStore.Bridge;
using EventStore.Commands;
using EventStore.ExternalHandlers;
using EventStore.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore.Extensions
{
    public static class AddEventStore
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            AddCore.AddServices(services);
            
            services.AddTransient<IEventStoreBridge, EventStoreBridge>();
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(AddEventStore).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(BaseEventCommand).Assembly,
                typeof(AppendEvevntHandler).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(INotification).Assembly,
                typeof(UserConnetedHandler).Assembly));
            
            
            
            return services; 
        }
    }
}
