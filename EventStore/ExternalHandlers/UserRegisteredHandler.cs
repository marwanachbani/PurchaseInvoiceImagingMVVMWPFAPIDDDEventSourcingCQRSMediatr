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
    public class UserRegisteredHandler : INotificationHandler<UserRegistered>
    {
        private readonly IEventStoreBridge _eventStoreBridge;

        public UserRegisteredHandler(IEventStoreBridge eventStoreBridge)
        {
            _eventStoreBridge = eventStoreBridge;
        }

        public async Task Handle(UserRegistered notification, CancellationToken cancellationToken)
        {
            await _eventStoreBridge.Store(notification);
        }
    }
}
