using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Core.Events
{
    public class ModifyCalcFailed : BaseEvent
    {
        public ModifyCalcFailed()
        {
            Name = this.GetType().Name;
        }
    }
}
