using Core.Events;
using EventStore.Bridge;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore.ExternalHandlers
{
    public class UserConnetedHandler : INotificationHandler<UserLogedIn>
    {
        private readonly IEventStoreBridge _eventStoreBridge;
        private readonly ILogger<UserConnetedHandler> _logger;

        public UserConnetedHandler(IEventStoreBridge eventStoreBridge, ILogger<UserConnetedHandler> logger)
        {
            _eventStoreBridge = eventStoreBridge;
            _logger = logger;
        }

        public async Task Handle(UserLogedIn notification, CancellationToken cancellationToken)
        {
            try
            {
                await _eventStoreBridge.Store(notification);
            }
            catch (Exception )
            {
                
                throw;
            }
            
        }
    }
}
