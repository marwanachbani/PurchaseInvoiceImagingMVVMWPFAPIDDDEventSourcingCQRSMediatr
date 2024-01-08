namespace Server.Commands
{
    public class ReAddinvoice
    {
        public ReAddinvoice(Guid id, byte[] imageData, string committedBy)
        {
            Id = id;
            ImageData = imageData;
            CommittedBy = committedBy;
        }

        public Guid Id { get; set; }
        public byte[] ImageData { get; set; }
        public string CommittedBy { get; set; }
    }
}
