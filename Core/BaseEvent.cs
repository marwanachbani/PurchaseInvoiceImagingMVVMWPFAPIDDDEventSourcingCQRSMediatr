using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace Core
{
    public abstract class BaseEvent : INotification
    {
        public Guid Id { get ; set; }
        
        public string CommittedBy { get ; set; }
        public BaseEvent Data { get; set; }
        public string Name { get; set; } 
        public DateTime Date { get; set; } = DateTime.Now; 
        public string Aggregate { get; set; }
        public string Response { get; set; }
        public int Version { get; set; }
    }
}
