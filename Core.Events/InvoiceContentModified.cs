using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class InvoiceContentModified : BaseEvent
    {
        public InvoiceContentModified()
        {
            Name = this.GetType().Name;
        }
        public DateTime ContentDate { get; set; }
       
    }
}
