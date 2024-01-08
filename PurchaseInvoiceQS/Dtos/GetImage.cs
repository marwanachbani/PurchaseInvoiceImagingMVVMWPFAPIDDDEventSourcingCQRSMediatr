using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseInvoiceQS.Dtos
{
    public class GetImage
    {
        public byte[] Picture { get; set; }

        public GetImage(byte[] picture)
        {
            Picture = picture;
        }
    }
}
