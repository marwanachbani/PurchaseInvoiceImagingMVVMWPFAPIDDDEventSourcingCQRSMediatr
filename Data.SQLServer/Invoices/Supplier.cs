using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class Supplier : BaseEntity
    {
        public Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; }
        public string SupplierName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }

       

        
}
