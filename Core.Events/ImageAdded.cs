using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class ImageAdded : BaseEvent
    {
        public ImageAdded()
        {
            Name = this.GetType().Name;
        }
        public byte[] ImageData { get; set; }
        public Guid Content { get; set; }
    }
}
