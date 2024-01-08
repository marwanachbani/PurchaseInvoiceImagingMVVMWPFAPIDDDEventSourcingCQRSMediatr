using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class DeleteCalcFailed : BaseEvent
    {
        public DeleteCalcFailed()
        {
            Name = this.GetType().Name; 
        }
    }
}
