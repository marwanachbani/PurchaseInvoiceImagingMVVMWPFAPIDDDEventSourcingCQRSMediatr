using Core;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore.Bridge
{
    public class EventStoreBridge : IEventStoreBridge
    {
        private readonly IMediator _mediator;

        public EventStoreBridge(IMediator mediator)
        {
            _mediator = mediator;
        }

        
        public async Task Store(BaseEvent @event)
        {
            var aggregate = new EventSourcingAggregate(_mediator);
            await aggregate.SendappendeventCommand(@event);
            
        }
    }
    public interface IEventStoreBridge
    {
        Task Store(BaseEvent @event); 
    }
}
