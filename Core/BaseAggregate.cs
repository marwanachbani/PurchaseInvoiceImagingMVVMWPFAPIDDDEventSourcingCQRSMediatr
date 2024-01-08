using MediatR;
using System.Runtime.CompilerServices;

namespace Core
{
    public abstract class BaseAggregate
    {
        private IMediator mediator ;

        protected BaseAggregate(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected void PublishEvent(BaseEvent @event)
        {
            mediator.Publish(@event);
        }
        protected async Task<BaseResponse> SendCommand(BaseCommand command)
        {
            var response = await mediator.Send(command);
            
            return response; 
        }
        protected async Task SendCommand(BaseEventCommand command)
        {
             await mediator.Send(command);
        }
    }
}