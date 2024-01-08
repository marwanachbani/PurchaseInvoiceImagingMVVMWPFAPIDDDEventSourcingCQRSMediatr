using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class InvoiceDeleted : BaseEvent
    {
        public InvoiceDeleted()
        {
            Name = this.GetType().Name;
        }
    }
}
