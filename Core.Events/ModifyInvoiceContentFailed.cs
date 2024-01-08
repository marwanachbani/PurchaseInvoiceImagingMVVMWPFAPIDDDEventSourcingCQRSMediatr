using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ModifyInvoiceContentFailed : BaseEvent
    {
        public ModifyInvoiceContentFailed()
        {
            Name = this.GetType().Name;
        }
        public DateTime ContentDate { get; set; }
       
    }
}
