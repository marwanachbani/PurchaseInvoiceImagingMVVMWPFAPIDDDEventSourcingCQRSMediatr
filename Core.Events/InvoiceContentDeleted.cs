using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class InvoiceContentDeleted : BaseEvent
    {
        public InvoiceContentDeleted()
        {
            Name = this.GetType().Name;
        }
        public string ContentDate { get; set; }
      
    }
}
