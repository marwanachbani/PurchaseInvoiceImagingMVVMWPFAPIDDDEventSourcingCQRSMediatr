using Data.SQLServer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using PurchaseInvoiceQS.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceQS.Queries
{
    public class GetAllQuery
    {
        public class Query : IRequest<List<GetAll>>
        {

        }
        public class QueryHandler : IRequestHandler<Query, List<GetAll>>
        {
            private readonly AppDbContext context = new AppDbContext();
            public async Task<List<GetAll>> Handle(Query request, CancellationToken cancellationToken)
            {
                var result = new List<GetAll>();
                var list = await context.InvoiceContents.Include(b => b.Invoices).ToListAsync();

                foreach (var item in list)
                {
                    var invoices = new List<string>();
                    foreach (var inv in item.Invoices)
                    {
                        invoices.Add(inv.Id.ToString());
                    }
                    result.Add(new GetAll { Content = item.Contenet, Invoices = invoices }
                        );
                }
                return result;
            }
        }

    }
}
