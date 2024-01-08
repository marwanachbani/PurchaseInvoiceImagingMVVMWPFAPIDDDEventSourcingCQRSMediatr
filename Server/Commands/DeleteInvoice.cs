namespace Server.Commands
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
