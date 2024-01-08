using Core;
using Core.Exceptions;
using Data.SQLServer.Invoices;
using MediatR;
using MediatR.Pipeline;
using PurchaseInvoiceCS.Commands;


namespace PurchaseInvoiceCS.Handlers
{
    public class ChangeInvoiceContentForInvoiceHandler : IRequestHandler<ChangeInvoiceContentForInvice, BaseResponse>
    {
        private readonly IInvoiceRepository _invoice;

        public ChangeInvoiceContentForInvoiceHandler(IInvoiceRepository invoice, IMediatrException<ChangeInvoiceContentForInvice, string, Exception> requestExceptionHandler)
        {
            _invoice = invoice;
            this.requestExceptionHandler = requestExceptionHandler;
        }

        private readonly IMediatrException<ChangeInvoiceContentForInvice, string, Exception> requestExceptionHandler;
        public async Task<BaseResponse> Handle(ChangeInvoiceContentForInvice request, CancellationToken cancellationToken)
        {
            var command = await _invoice.ChangeContent(request.Id, request.ContentId, request.UpdatedBy);
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
                var e = await requestExceptionHandler.Handle(request, ex, state, cancellationToken);
                response.Successed = false;
                response.Response = e;
                await Task.CompletedTask;
                return response;
            }
        }
    }
}
