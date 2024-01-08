using Core;
using Core.Exceptions;
using Data.SQLServer.Invoices;
using MediatR;
using MediatR.Pipeline;
using PurchaseInvoiceCS.Commands;


namespace PurchaseInvoiceCS.Handlers
{
    public class CreateInvoiceContentHandler : IRequestHandler<CreateInvoiceContent, BaseResponse>
    {
        private readonly IInvoiceContentRepository _content;
        private readonly IMediatrException<CreateInvoiceContent, string, Exception> requestExceptionHandler;

        public CreateInvoiceContentHandler(IInvoiceContentRepository content, IMediatrException<CreateInvoiceContent, string, Exception> requestExceptionHandler)
        {
            _content = content;
            this.requestExceptionHandler = requestExceptionHandler;
        }

        public async Task<BaseResponse> Handle(CreateInvoiceContent request, CancellationToken cancellationToken)
        {
            var command = await _content.AddDateInvoice(request.Id,request.ContentDate);
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
                    var e = await requestExceptionHandler.Handle(request, ex, state, cancellationToken);
                    response.Successed = false;
                    response.Response = e;
                    await Task.CompletedTask;
                    return response;
                }
            
            return response;

        }
    }
}
