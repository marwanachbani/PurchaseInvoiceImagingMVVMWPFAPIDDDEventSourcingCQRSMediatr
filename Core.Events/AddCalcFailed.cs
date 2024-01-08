using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Events
{
    public class AddCalcFailed : BaseEvent
    {

        public AddCalcFailed()
        {
            Name = this.GetType().Name;
        }
    }
}
