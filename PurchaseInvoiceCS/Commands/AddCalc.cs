using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Commands
{
    public class AddCalc : BaseCommand
    {
        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public decimal Subtotal { get; set; }
        public int TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public int RedRate { get; set; }
        public decimal ReductAmunt { get; set; }
        public decimal Total { get; set; }
       
    }
}
