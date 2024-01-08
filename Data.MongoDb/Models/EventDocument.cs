using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MongoDb.Models
{
    public class EventDocument:BaseModel
    {
        public EventDocument()
        {
            
        }
        public EventDocument(Guid id,string aggregateId, string aggregateName, string eventName, string committedBy, string data, string response, DateTime occuredAt)
        {
            Id = id;
        
            AggregateId = aggregateId;
            AggregateName = aggregateName;
            EventName = eventName;
            CommittedBy = committedBy;
            Data = data;
            Response = response;
            OccuredAt = occuredAt;
        }

        public string AggregateId { get; set; }
        public string AggregateName { get; set; }
        public string EventName { get; set; }
        public string CommittedBy { get; set; }
        public string Data { get; set; }
        public string Response { get; set; }
        public DateTime OccuredAt { get; set; }
    }
}
