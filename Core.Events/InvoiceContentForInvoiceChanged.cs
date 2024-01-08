using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class InvoiceContentForInvoiceChanged : BaseEvent
    {
        public InvoiceContentForInvoiceChanged()
        {
            Name = this.GetType().Name;
        }
        public Guid ContentId { get; set; }
        
    }
}
