using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class AddInvoiceContent
    {
        public DateTime Date { get; set; }
        public string CommittedBy { get; set; }

        public AddInvoiceContent(DateTime date, string committedBy)
        {
            Date = date;
            CommittedBy = committedBy;
        }
    }
}
