using Core;
using Core.Exceptions;
using Data.SQLServer.Invoices;
using MediatR;
using MediatR.Pipeline;
using PurchaseInvoiceCS.Commands;


namespace PurchaseInvoiceCS.Handlers
{
    public class DeleteInvoiceContentHandler : IRequestHandler<DeleteInvoiceContent, BaseResponse>
    {
        private readonly IInvoiceContentRepository _content;
        private readonly IMediatrException<DeleteInvoiceContent, string, Exception> _requestExceptionHandler;

        public DeleteInvoiceContentHandler(IInvoiceContentRepository content, IMediatrException<DeleteInvoiceContent, string, Exception> requestExceptionHandler)
        {
            _content = content;
            _requestExceptionHandler = requestExceptionHandler;
        }

        public async Task<BaseResponse> Handle(DeleteInvoiceContent request, CancellationToken cancellationToken)
        {
            var command = await _content.DeleteDateInvoice(request.Id);
            var response = new BaseResponse();
           
                try
                {
                    response.Successed = command.Successed;
                    response.Response = command.Response;
                    await Task.CompletedTask;

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
            
            return response;
        }
    }
}
