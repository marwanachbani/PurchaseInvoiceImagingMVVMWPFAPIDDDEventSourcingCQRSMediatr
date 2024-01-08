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
    public class UserActivatedHandler : INotificationHandler<UserActivated>
    {
        private readonly IEventStoreBridge _eventStoreBridge;

        public UserActivatedHandler(IEventStoreBridge eventStoreBridge)
        {
            _eventStoreBridge = eventStoreBridge;
        }

        public async Task Handle(UserActivated notification, CancellationToken cancellationToken)
        {
            await _eventStoreBridge.Store(notification);
        }
    }
}
