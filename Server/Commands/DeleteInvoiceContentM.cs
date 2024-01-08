namespace Server.Commands
{
    public class DeleteInvoiceContentM
    {
        public DeleteInvoiceContentM(string content, string committedBy)
        {
            Content = content;
            CommittedBy = committedBy;
        }

        public string Content { get; set; }
        public string CommittedBy { get; set; }
    }
}
