using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ItemModified : BaseEvent
    {
        public ItemModified()
        {
            Name = this.GetType().Name;
        }
        public Guid InvoiceId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
