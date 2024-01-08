
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.CommandModels
{
    public class ItemCommandModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        
        public Guid InvoiceId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
       
        public decimal Price { get; set; }
    }
}
