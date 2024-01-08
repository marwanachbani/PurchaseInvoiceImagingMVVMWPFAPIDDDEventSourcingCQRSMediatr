using Core.Extensions;
using Data.SQLServer;
using Data.SQLServer.Exceptions;
using Data.SQLServer.Invoices;
using Data.SQLServer.Results;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PurchaseInvoiceCS.Bridge;
using PurchaseInvoiceCS.Commands;
using PurchaseInvoiceCS.ExternalHandlers;
using PurchaseInvoiceCS.Handlers;
using ServiceStack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Extensions
{
    public static class AddPurchaseCS
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPurchaseInvoiceCommandBridge, PurchaseInvoiceCommandBridge>();
            services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(AddPurchaseCS).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(CreateInvoice).Assembly,
                typeof(CreateInvoiceHandler).Assembly));
            services.AddMediatR(x => x.RegisterServicesFromAssemblies(typeof(INotification).Assembly,
                typeof(OnImageAddForInvoice).Assembly));
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IInvoiceContentRepository, InvoiceContentRepository>();
            services.AddTransient<IInvoiceCalculationRepository, InvoiceCalculationRepository>();
            services.AddTransient<IInvoiceItemRepository, InvoiceItemRepository>();
            services.AddTransient<IInvoiceSupplierRepository, InvoiceSupplierRepository>();
            services.AddTransient<IDbResult, DbResults>();
            services.AddTransient<IDbContextException, DbContextExceptions>();
            DependencyInjection.GetDataServices(services);
            AddCore.AddServices(services);
            return services;
            
        }
    }
}
