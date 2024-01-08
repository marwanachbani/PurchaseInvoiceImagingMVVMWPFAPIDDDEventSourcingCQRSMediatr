using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Responses
{
    public class All
    {
        public All(string content, List<string> invoices)
        {
            Content = content;
            Invoices = invoices;
        }

        public string Content { get; set; }
        public List<string> Invoices { get; set; }
    }
}
