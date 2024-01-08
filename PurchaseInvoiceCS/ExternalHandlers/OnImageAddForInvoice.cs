using Core.Events;
using Data.SQLServer.Invoices;
using MediatR;
using Microsoft.Extensions.Logging;
using PurchaseInvoiceCS.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.ExternalHandlers
{
    public class OnImageAddForInvoice : INotificationHandler<ImageAddForInvoice>
    {
        private readonly IPurchaseInvoiceCommandBridge invoiceRepository;
        private readonly ILogger<OnImageAddForInvoice> logger;

        public OnImageAddForInvoice(IPurchaseInvoiceCommandBridge invoiceRepository, ILogger<OnImageAddForInvoice> logger)
        {
            this.invoiceRepository = invoiceRepository;
            this.logger = logger;
        }

        public async Task Handle(ImageAddForInvoice notification, CancellationToken cancellationToken)
        {
            var response = await this.invoiceRepository.AddInvoice(notification.Id,notification.ImageData,notification.CommittedBy,notification.Content);
            if (response.Successed == true)
            {
                this.logger.LogError(response.Response);
            }

        }
    }
}
