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
    public class AddSupplierHandler : IRequestHandler<AddSupplier, BaseResponse>
    {
        private readonly IInvoiceSupplierRepository _supplier;
        public AddSupplierHandler(IInvoiceSupplierRepository sup)
        {
            _supplier = sup;
        }
        public async Task<BaseResponse> Handle(AddSupplier request, CancellationToken cancellationToken)
        {
            var response = await _supplier.AddSupplier(request.Id, request.InvoiceId , request.Supllier , request.Street
                ,request.City , request.State, request.Country , request.ZipCode);
            return response;
        }
    }
}
