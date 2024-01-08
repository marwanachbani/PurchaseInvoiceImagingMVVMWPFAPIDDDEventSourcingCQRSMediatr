using Core;
using Core.Exceptions;
using Data.SQLServer.Invoices;
using MediatR;
using MediatR.Pipeline;
using PurchaseInvoiceCS.Commands;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Handlers
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoice, BaseResponse>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediatrException<CreateInvoice,string,Exception> requestExceptionHandler;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository, IMediatrException<CreateInvoice, string, Exception> requestExceptionHandler)
        {
            _invoiceRepository = invoiceRepository;
            this.requestExceptionHandler = requestExceptionHandler;
        }

        public async Task<BaseResponse> Handle(CreateInvoice request, CancellationToken cancellationToken)
        {
            var command = await _invoiceRepository.AddInvoice(request.Id,  request.ContentId, request.ScannedBy);
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
                var e = await requestExceptionHandler.Handle(request,ex,state , cancellationToken);
                response.Successed = false;
                response.Response = e;
                await Task.CompletedTask;
                return response; 
            }
            
        }
    }
    
}
