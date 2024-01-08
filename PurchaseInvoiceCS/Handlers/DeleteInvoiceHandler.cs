using Core;
using Core.Exceptions;
using Data.SQLServer.Invoices;
using MediatR;
using MediatR.Pipeline;
using PurchaseInvoiceCS.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Handlers
{
    public class DeleteInvoiceHandler : IRequestHandler<DeleteInvoice, BaseResponse>
    {
        private readonly IInvoiceRepository _invoice;
        private readonly IMediatrException<DeleteInvoice, string, Exception> _requestExceptionHandler;

        public DeleteInvoiceHandler(IInvoiceRepository invoice, IMediatrException<DeleteInvoice, string, Exception> requestExceptionHandler)
        {
            _invoice = invoice;
            _requestExceptionHandler = requestExceptionHandler;
        }

        public async Task<BaseResponse> Handle(DeleteInvoice request, CancellationToken cancellationToken)
        {
            var command = await _invoice.DeleteInvoice(request.Id);
            var response = new BaseResponse();
            try
            {
                response.Successed = command.Successed;
                response.Response = command.Response;
                await Task.CompletedTask;
                return response;
            }
            catch (Exception ex)
            {
                var state = new RequestExceptionHandlerState<string>();
                var e = await _requestExceptionHandler.Handle(request, ex, state, cancellationToken);
                response.Successed = false;
                response.Response = e;
                await Task.CompletedTask;
                return response;
            }
            
        }
    }
}
