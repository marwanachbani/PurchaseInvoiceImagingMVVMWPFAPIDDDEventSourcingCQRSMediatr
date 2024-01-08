using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class CreateInvoiceContentFailed : BaseEvent
    {
        public CreateInvoiceContentFailed()
        {
            Name = this.GetType().Name;
        }
        public DateTime ContentDay { get; set; }
        
    }
}
