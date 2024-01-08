using Core;
using EventStore.Commands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public class EventSourcingAggregate : BaseAggregate
    {
        private Guid _id;
        private string _committedBy;
        private BaseEvent _data;
        private string _name;
        private DateTime _date;
        private string _aggregate;
        private string _response;
        private int _version;
        private string _cUID;

        public Guid Id { get => _id; set => _id = value; }
        public string CUID { get => _cUID; set => _cUID = value; }
        public string CommittedBy { get => _committedBy; set => _committedBy = value; }
        public BaseEvent Data { get => _data; set => _data = value; }
        public string Name { get => _name; set => _name = value; }
        public DateTime Date { get => _date; set => _date = value; }
        public string Aggregate { get => _aggregate; set => _aggregate = value; }
        public string Response { get => _response; set => _response = value; }
        public int Version { get => _version; set => _version = value; }
        public EventSourcingAggregate(IMediator mediator) : base(mediator)
        {
        }
        public async Task SendappendeventCommand(BaseEvent @event)
        {
            await SetAppendEventParameter(@event);
            await this.SendCommand(new AppendEvent
            {
                Id = _id,
                Aggregate = _aggregate,
                CommittedBy = _committedBy,
                Data = _data,
                Name = _name,
                DateTime = _date,
                Response = _response,
                Version = _version,
                
                
            }); ;
        }
        private async Task SetAppendEventParameter(BaseEvent @event)
        {
            _id = @event.Id;
            _data = @event;
            _committedBy = @event.CommittedBy;
            _aggregate = @event.Aggregate;
            _response = @event.Response;
            _date = @event.Date;
            _version = @event.Version;
            _name = @event.Name;
            
            await Task.CompletedTask; 
        }
        
    }
}
