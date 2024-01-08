using Core.Events;
using MediatR;
using PurchaseInvoiceCS.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.ExternalHandlers
{
    public class ImageAddedForInvoiceHandler : INotificationHandler<ImageAdded>
    {
        private readonly IPurchaseInvoiceCommandBridge _purchaseInvoiceCommandBridge;

        public ImageAddedForInvoiceHandler(IPurchaseInvoiceCommandBridge purchaseInvoiceCommandBridge)
        {
            _purchaseInvoiceCommandBridge = purchaseInvoiceCommandBridge;
        }

        public async Task Handle(ImageAdded notification, CancellationToken cancellationToken)
        {
            await _purchaseInvoiceCommandBridge.AddInvoice(notification.Id, notification.ImageData,notification.CommittedBy,notification.Content);
        }
    }
}
