using Core.Events;
using EventStore.Bridge;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore.ExternalHandlers
{
    public class DeleteInvoiceContentFailedHandler : INotificationHandler<DeleteInvoiceContentFailed>
    {
        private readonly IEventStoreBridge _eventStoreBridge;

        public DeleteInvoiceContentFailedHandler(IEventStoreBridge eventStoreBridge)
        {
            _eventStoreBridge = eventStoreBridge;
        }

        public async Task Handle(DeleteInvoiceContentFailed notification, CancellationToken cancellationToken)
        {
            await _eventStoreBridge.Store(notification);
        }
    }
}
