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
    public class InvoiceCreatedHandlerForEventSourcing : INotificationHandler<InvoiceCreated>
    {
        private readonly IEventStoreBridge _bridge;

        public InvoiceCreatedHandlerForEventSourcing(IEventStoreBridge bridge)
        {
            _bridge = bridge;
        }

        public async Task Handle(InvoiceCreated notification, CancellationToken cancellationToken)
        {
            await _bridge.Store(notification); 
        }
    }
}
