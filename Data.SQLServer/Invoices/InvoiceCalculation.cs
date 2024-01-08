using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class InvoiceCalculation : BaseEntity
    {
        public Invoice Invoice { get; set; }
        public Guid InvoiceId { get; set; }
        [Precision(18, 2)]
        public decimal SubTotal { get; set; }
        public int TaxRate { get; set; }
        [Precision(18, 2)]
        public decimal TaxAmount { get; set; }
        public int ReductionRate { get; set; }
        [Precision(18, 2)]
        public decimal ReductionAmount { get; set; }
        [Precision(18, 2)]
        public decimal Total { get; set; }
    }
}
