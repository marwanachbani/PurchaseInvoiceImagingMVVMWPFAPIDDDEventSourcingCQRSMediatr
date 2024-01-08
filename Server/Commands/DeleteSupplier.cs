namespace Server.Commands
{
    public class DeleteSupplier
    {
        public DeleteSupplier(Guid id, string commmittedBy)
        {
            Id = id;
            CommmittedBy = commmittedBy;
        }

        public Guid Id { get; set; }
        public string CommmittedBy { get; set; }
    }
}
