using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Invoices
{
    public class InvoiceContent : BaseEntity
    {
        public InvoiceContent()
        {
            
        }
        public DateTime Date { get; set; }
        public string Contenet { get; set; }
        public List<Invoice> Invoices { get; set; }
        public bool AllIndexed { get; set; }
    }
}
