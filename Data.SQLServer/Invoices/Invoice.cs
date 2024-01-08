using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class Invoice : BaseEntity
    {

        
        public string InvoiceNumber { get; set; }
        public Supplier Supplier { get; set; }
        public List<Item> Items { get; set; }
        public InvoiceContent InvoiceContent { get; set; }
        public Guid InvoiceContentId { get; set; }

        public InvoiceCalculation InvoiceCalculation { get; set; }
        public string ScannedBy { get; set; }
        public DateTime ScannedAt { get; set; }
        public DateTime LastScanUpdate { get; set; }
        public string LastScanBy { get; set; }
        public string IndexStatut { get; set; }
        public string IndexedBy { get; set; }
        public DateTime LastUpdateAt { get; set; }
        public string LastUpdateBy { get; set; }

        public int Version { get; set; } = 1;

    }
}
