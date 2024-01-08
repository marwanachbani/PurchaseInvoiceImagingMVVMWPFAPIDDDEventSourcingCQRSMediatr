using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Commands
{
    public class DeleteCalc : BaseCommand
    {
        public Guid InvoiceId { get; set; }
    }
}
