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
    public class DeleteCalcHandler : IRequestHandler<DeleteCalc, BaseResponse>
    {
        private readonly IInvoiceCalculationRepository _calc;

        public DeleteCalcHandler(IInvoiceCalculationRepository calc)
        {
            _calc = calc;
        }

        public async Task<BaseResponse> Handle(DeleteCalc request, CancellationToken cancellationToken)
        {
            var res = await _calc.DeleteInvoiceCalc(request.InvoiceId);
            return res;
        }
    }
}
