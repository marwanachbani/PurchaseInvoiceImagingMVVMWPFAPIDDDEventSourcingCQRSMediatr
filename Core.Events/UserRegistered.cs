using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class UserRegistered : BaseEvent
    {
        public UserRegistered()
        {
            Name = this.GetType().Name;
        }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
    }
}
