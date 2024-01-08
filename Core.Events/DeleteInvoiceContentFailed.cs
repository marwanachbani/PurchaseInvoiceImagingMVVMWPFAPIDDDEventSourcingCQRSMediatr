using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class DeleteInvoiceContentFailed : BaseEvent
    {
        public DeleteInvoiceContentFailed()
        {
            Name = this.GetType().Name;
        }
        public string ContentDate { get; set; }
        
    }
}
