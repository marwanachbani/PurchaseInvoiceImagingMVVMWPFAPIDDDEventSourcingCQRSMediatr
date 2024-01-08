

namespace Core.Events
{
    public class InvoiceCreated : BaseEvent
    {
        public InvoiceCreated()
        {
            Name = this.GetType().Name;
        }
        public byte[] Image { get; set; }
        
        public Guid ContentId { get; set; }
    }
}