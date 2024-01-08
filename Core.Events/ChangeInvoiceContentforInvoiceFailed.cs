using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ChangeInvoiceContentforInvoiceFailed : BaseEvent
    {
        public ChangeInvoiceContentforInvoiceFailed()
        {
            Name = this.GetType().Name;
        }
        public Guid ContentId { get; set; }
       
    }
}
