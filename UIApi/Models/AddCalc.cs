using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class AddCalc
    {
        public AddCalc(Guid invoiceId, decimal subtotal, int taxRate, decimal taxAmount, int redRate, decimal reductAmunt, decimal total, string committedBy)
        {
            InvoiceId = invoiceId;
            Subtotal = subtotal;
            TaxRate = taxRate;
            TaxAmount = taxAmount;
            RedRate = redRate;
            ReductAmunt = reductAmunt;
            Total = total;
            CommittedBy = committedBy;
        }

        public Guid InvoiceId { get; set; }
        public decimal Subtotal { get; set; }
        public int TaxRate { get; set; }
        public decimal TaxAmount { get; set; }
        public int RedRate { get; set; }
        public decimal ReductAmunt { get; set; }
        public decimal Total { get; set; }
        public string CommittedBy { get; set; }
    }
}
