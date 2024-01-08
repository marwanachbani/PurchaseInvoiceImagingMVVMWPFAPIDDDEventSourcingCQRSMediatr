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
    public class AddInvoiceNumberHandler : IRequestHandler<AddInvoiceNumber, BaseResponse>
    {
        private readonly IInvoiceRepository _invoice;

        public AddInvoiceNumberHandler(IInvoiceRepository invoice)
        {
            _invoice = invoice;
        }

        public async Task<BaseResponse> Handle(AddInvoiceNumber request, CancellationToken cancellationToken)
        {
            var res = await _invoice.AddInvoiceNumber(request.Id, request.InvoiceNumber);
            return new BaseResponse { Successed = res.Successed , Response = res.Response };
        }
    }
}
