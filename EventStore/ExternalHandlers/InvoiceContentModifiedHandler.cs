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
    internal class InvoiceContentModifiedHandler : INotificationHandler<InvoiceContentModified>
    {
        private readonly IEventStoreBridge _eventStoreBridge;

        public InvoiceContentModifiedHandler(IEventStoreBridge eventStoreBridge)
        {
            _eventStoreBridge = eventStoreBridge;
        }

        public async Task Handle(InvoiceContentModified notification, CancellationToken cancellationToken)
        {
            await _eventStoreBridge.Store(notification);
        }
    }
}
