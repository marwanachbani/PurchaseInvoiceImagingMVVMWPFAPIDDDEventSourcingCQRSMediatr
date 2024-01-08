using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ItemDeleted : BaseEvent
    {
        public ItemDeleted()
        {
            Name = this.GetType().Name;
        }
        public Guid InvoiceId { get; set; }
    }
}
