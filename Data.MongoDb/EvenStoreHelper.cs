using Data.MongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MongoDb
{
    public class EvenStoreHelper
    {
        

        

        
        public async Task StoreEvent(EventDocument @event)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("InvoiceEventStore");
            var eventCollection = database.GetCollection<EventDocument>("Events");

            await eventCollection.InsertOneAsync(@event);
        }
        public async Task<List<EventDocument>> GetEventsByAggregate(Guid aggregateId)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("InvoiceEventStore");
            var eventCollection = database.GetCollection<EventDocument>("Events");

            var filter = Builders<EventDocument>.Filter.Eq("_id", aggregateId);
            var events = await eventCollection.Find(filter).ToListAsync();

            return events;
        }
    }

    
}
