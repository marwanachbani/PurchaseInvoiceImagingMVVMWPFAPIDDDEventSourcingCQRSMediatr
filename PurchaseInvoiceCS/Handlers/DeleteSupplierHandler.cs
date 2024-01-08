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
    public class DeleteSupplierHandler : IRequestHandler<DeleteSupplier, BaseResponse>
    {
        private readonly IInvoiceSupplierRepository _sup;

        public DeleteSupplierHandler(IInvoiceSupplierRepository sup)
        {
            _sup = sup;
        }

        public Task<BaseResponse> Handle(DeleteSupplier request, CancellationToken cancellationToken)
        {
            var response = _sup.DeleteSupplier(request.Id);
            return response; 
        }
    }
}
