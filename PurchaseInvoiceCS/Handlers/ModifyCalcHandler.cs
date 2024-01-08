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
    public class ModifyCalcHandler : IRequestHandler<ModifyCalc, BaseResponse>
    {
        private readonly IInvoiceCalculationRepository _calc;

        public ModifyCalcHandler(IInvoiceCalculationRepository calc)
        {
            _calc = calc;
        }

        public async Task<BaseResponse> Handle(ModifyCalc request, CancellationToken cancellationToken)
        {
            var response = await _calc.ModifyInvoiceCalc(request.InvoiceId, request.Subtotal, request.TaxRate
                , request.TaxAmount, request.RedRate, request.ReductAmunt, request.Total);
            return response;
        }
    }
}
