using Amazon.Runtime.Internal;
using Core;
using Data.SQLServer.Invoices;
using MediatR;
using PurchaseInvoiceCS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Handlers
{
    public class ModifySupplierHandler : IRequestHandler<ModifySupplier, BaseResponse>
    {
        private readonly IInvoiceSupplierRepository _sup;

        public ModifySupplierHandler(IInvoiceSupplierRepository sup)
        {
            _sup = sup;
        }

        public async Task<BaseResponse> Handle(ModifySupplier request, CancellationToken cancellationToken)
        {
            var response = await _sup.UpdateSupplier(request.InvoiceId, request.Supllier, request.Street, request.City, request.State, request.Country, request.ZipCode);
            return response;
        }
    }
}
