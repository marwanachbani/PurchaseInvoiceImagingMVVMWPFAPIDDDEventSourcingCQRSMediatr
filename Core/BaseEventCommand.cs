using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BaseEventCommand : IRequest
    {
        
        public Guid Id { get; set; }
        public string CommittedBy { get ; set; }
        public BaseEvent Data { get ; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Aggregate { get; set; }
        public string Response { get; set; }
        public int Version { get; set; }
    }
}
