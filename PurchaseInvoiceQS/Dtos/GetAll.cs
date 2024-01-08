using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceQS.Dtos
{
    public class GetAll 
    {
        public string Content { get; set; }
        public List<string> Invoices { get; set; }
    }
}
