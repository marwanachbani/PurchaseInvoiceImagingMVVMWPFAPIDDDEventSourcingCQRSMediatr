namespace Server.Commands
{
    public class EditSupplier
    {
        public EditSupplier(Guid id, Guid invoiceId, string supllier, string street, string city, string state, string country, string zipCode, string committedby)
        {
            Id = id;
            InvoiceId = invoiceId;
            Supllier = supllier;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            Committedby = committedby;
        }

        public Guid Id { get; set; }
        public Guid InvoiceId { get; set; }
        public string Supllier { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public string Committedby { get; set; }
    }
}
