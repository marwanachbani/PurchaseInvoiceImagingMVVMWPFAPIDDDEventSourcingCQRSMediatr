namespace Server.Commands
{
    public class AddInvoiceContent
    {
        public AddInvoiceContent(DateTime date, string committedBy)
        {
            Date = date;
            CommittedBy = committedBy;
        }

        public DateTime Date { get; set; }
        public string CommittedBy { get; set; }
    }
}
