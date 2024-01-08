using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class AddSup
    {
        public AddSup(Guid invoiceId, string invoiceNumber, string supllier, string street, string city, string state, string country, string zipCode, string committedby)
        {
            InvoiceId = invoiceId;
            InvoiceNumber = invoiceNumber;
            Supllier = supllier;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Committedby = committedby;
        }

        public Guid InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public string Supllier { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Committedby { get; set; }
    }
}
