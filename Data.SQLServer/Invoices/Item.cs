using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class Item : BaseEntity
    {
        public Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; }

        public string ProductName { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
    }
        
}
