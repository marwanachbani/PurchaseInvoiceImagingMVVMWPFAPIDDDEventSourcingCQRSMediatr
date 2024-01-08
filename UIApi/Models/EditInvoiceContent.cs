using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class EditInvoiceContent
    {
        public EditInvoiceContent(string content, DateTime date, string committedBy)
        {
            Content = content;
            Date = date;
            CommittedBy = committedBy;
        }

        public string Content { get; set; }
        public DateTime Date { get; set; }
        public string CommittedBy { get; set; }
    }
}
