using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ModifyItemFailed : BaseEvent
    {
        public ModifyItemFailed()
        {
            Name = this.GetType().Name;
        }
        public Guid InvoiceId { get; set; }
    }
}
