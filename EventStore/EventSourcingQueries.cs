using Data.MongoDb;
using Data.MongoDb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventStore
{
    public class EventSourcingQueries
    {
        private EvenStoreHelper eventStore = new();
        public async Task<List<EventDocument>> GetEvents(Guid id )
        {
            return await eventStore.GetEventsByAggregate( id );
        }
    }
}
