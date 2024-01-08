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
    internal class AddCalcHandler : IRequestHandler<AddCalc, BaseResponse>
    {
        private readonly IInvoiceCalculationRepository _calc;

        public AddCalcHandler(IInvoiceCalculationRepository calc)
        {
            _calc = calc;
        }

        public async Task<BaseResponse> Handle(AddCalc request, CancellationToken cancellationToken)
        {
            var response = await _calc.AddInvoiceCalc(request.Id,request.InvoiceId,request.Subtotal,request.TaxRate
                ,request.TaxAmount , request.RedRate , request.ReductAmunt , request.Total);
            return response;
        }
    }
}
