namespace Server.Commands
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
