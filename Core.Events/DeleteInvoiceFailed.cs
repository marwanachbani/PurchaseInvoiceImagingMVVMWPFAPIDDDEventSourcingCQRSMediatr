using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class DeleteInvoiceFailed : BaseEvent
    {
        public DeleteInvoiceFailed()
        {
            Name = this.GetType().Name;
        }
    }
}
