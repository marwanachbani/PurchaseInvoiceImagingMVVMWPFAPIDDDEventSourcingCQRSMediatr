using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class RegisterUserFailed : BaseEvent
    {
        public RegisterUserFailed()
        {
            Name = this.GetType().Name;
        }
        public string Email { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        
    }
}
