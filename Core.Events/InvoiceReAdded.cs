using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class InvoiceReAdded : BaseEvent
    {
        public InvoiceReAdded()
        {
            Name = this.GetType().Name;
        }
        public byte[] Image { get; set; }
        
        public Guid ContentId { get; set; }
    }
}
