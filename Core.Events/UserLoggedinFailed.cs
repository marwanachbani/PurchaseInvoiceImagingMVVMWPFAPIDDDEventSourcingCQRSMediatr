using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class UserLoggedinFailed : BaseEvent
    {
        public UserLoggedinFailed()
        {
            Name = this.GetType().Name;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
