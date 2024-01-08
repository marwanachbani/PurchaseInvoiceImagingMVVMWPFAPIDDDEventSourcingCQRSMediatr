using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Events
{
    public class UserLogedIn: BaseEvent
    {
        public UserLogedIn()
        {
            Name = this.GetType().Name;
        }
        public string UserName { get; set; }
    }
}
