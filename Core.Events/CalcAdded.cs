using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class CalcAdded : BaseEvent
    {
        public CalcAdded()
        {
            Name = this.GetType().Name;
        }
        public Guid InvoiceId { get; set; }
        public decimal Subtotal { get; set; }
        public int TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public int RedRate { get; set; }
        public decimal ReductAmunt { get; set; }
        public decimal Total { get; set; }
    }
}
