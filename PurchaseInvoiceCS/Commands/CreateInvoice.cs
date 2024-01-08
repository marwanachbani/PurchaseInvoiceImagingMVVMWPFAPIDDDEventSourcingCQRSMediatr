using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceCS.Commands
{
    public class CreateInvoice : BaseCommand
    {
        public Guid Id { get; set; }
        public string CUID { get; set; }
        public byte[] Image { get; set ; }
        public Guid ContentId { get ; set ; }
        public string ScannedBy { get ; set ; }
    }
}
