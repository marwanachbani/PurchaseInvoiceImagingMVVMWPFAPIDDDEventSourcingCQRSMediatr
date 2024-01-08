using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ImageUpdated : BaseEvent
    {
        public Guid InvoiceId { get; set; }
        public ImageUpdated()
        {
            Name = this.GetType().Name;
        }
    }
}
