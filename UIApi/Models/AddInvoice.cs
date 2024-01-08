using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class AddInvoice
    {
        public AddInvoice(byte[] picture, string content, string scannedBy)
        {
            Picture = picture;
            Content = content;
            ScannedBy = scannedBy;
        }

        public byte[] Picture { get; set; }
        public string Content { get; set; }
        public string ScannedBy { get; set; }
    }
}
