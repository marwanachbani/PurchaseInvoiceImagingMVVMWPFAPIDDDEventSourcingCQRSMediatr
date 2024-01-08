using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class SupplierAdded : BaseEvent
    {
        public SupplierAdded()
        {
            Name = this.GetType().Name;
        }
        public Guid InvoiceId { get; set; }
        public string Supllier { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
    }
}
