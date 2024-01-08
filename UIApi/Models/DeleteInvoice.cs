using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class DeleteInvoice
    {
        public DeleteInvoice(Guid id, string committedBy)
        {
            Id = id;
            CommittedBy = committedBy;
        }

        public Guid Id { get; set; }
        public string CommittedBy { get; set; }
    }
}
