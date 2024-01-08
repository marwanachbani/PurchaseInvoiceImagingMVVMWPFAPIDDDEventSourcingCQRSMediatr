using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class DeleteSupplierFailed : BaseEvent
    {
        public DeleteSupplierFailed()
        {
            Name = this.GetType().Name;
        }
        public Guid InvoiceId { get; set; }
    }
}
