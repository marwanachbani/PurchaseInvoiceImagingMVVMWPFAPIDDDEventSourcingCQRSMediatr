using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class DeleteItemFailed : BaseEvent
    {
        public DeleteItemFailed()
        {
            Name = this.GetType().Name;
        }
        public int MyProperty { get; set; }
    }
}
