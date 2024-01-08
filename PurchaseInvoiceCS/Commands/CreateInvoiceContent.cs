using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Commands
{
    public class CreateInvoiceContent : BaseCommand
    {
        public Guid Id { get; set; }
        public string CUID { get; set; }
        public DateTime ContentDate { get; set; }
    }
}
