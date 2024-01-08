using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ServiceStack.Text;
using System.Threading.Tasks;

namespace Core.EventSourcing
{
    public interface IEventStoreServices
    {
        string SerializeData<T>(T theEvent) where T : BaseEvent;
    }
    public class EventStoreServices : IEventStoreServices
    {
        public string SerializeData<T>(T theEvent) where T : BaseEvent
        {
            var data = JsonSerializer.SerializeToString(theEvent, typeof(BaseEvent));

            return data;
        }
    }
}
