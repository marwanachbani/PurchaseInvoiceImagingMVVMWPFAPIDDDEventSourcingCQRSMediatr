using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Events
{
    public class Event : BaseEntity
    {
        public Guid AggregateId { get; set; }
        public string AggregateName { get; set; }
        public string EventName { get; set; }
        public string CommittedBy { get; set; }
        public string Data { get; set; }
        public string Response { get; set; }
        public DateTime OccuredAt { get; set; }
    } 
}
