using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class ReAddInvoice
    {
        public ReAddInvoice(Guid id, byte[] imageData, string committedBy)
        {
            Id = id;
            ImageData = imageData;
            CommittedBy = committedBy;
        }

        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }
        public string CommittedBy { get; set; }
    }
}
