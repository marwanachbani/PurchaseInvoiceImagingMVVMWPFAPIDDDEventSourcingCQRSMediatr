using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfUI.Messages
{
    public class LogedMessage : EventArgs
    {
        public string Message { get; set; }

        public LogedMessage(string message)
        {
            Message = message;
        }
    }
}
