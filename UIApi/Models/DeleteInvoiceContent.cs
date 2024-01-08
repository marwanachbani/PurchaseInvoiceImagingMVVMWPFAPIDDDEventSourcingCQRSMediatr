using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class DeleteInvoiceContent
    {
        public DeleteInvoiceContent(string content, string committedBy)
        {
            Content = content;
            CommittedBy = committedBy;
        }

        public string Content { get; set; }
        public string CommittedBy { get; set; }
    }
}
