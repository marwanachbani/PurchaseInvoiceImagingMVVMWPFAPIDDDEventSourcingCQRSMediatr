using Core;
using Core.Exceptions;
using Data.SQLServer.Invoices;
using MediatR;
using MediatR.Pipeline;
using PurchaseInvoiceCS.Commands;


namespace PurchaseInvoiceCS.Handlers
{
    public class ModifyInvoiceContenthandler : IRequestHandler<ModifyinvoiceContent, BaseResponse>
    {
        private readonly IInvoiceContentRepository _invoiceContentRepository;
        private readonly IMediatrException<ModifyinvoiceContent, string, Exception> _requestExceptionHandler;

        public ModifyInvoiceContenthandler(IInvoiceContentRepository invoiceContentRepository, IMediatrException<ModifyinvoiceContent, string, Exception> requestExceptionHandler)
        {
            _invoiceContentRepository = invoiceContentRepository;
            _requestExceptionHandler = requestExceptionHandler;
        }

        public async Task<BaseResponse> Handle(ModifyinvoiceContent request, CancellationToken cancellationToken)
        {
            var command = await _invoiceContentRepository.ModifyDateInvoice(request.Id, request.ContentDAte);
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
