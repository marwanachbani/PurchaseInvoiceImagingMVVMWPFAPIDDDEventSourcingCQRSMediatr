using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twain
{
    public class MessageByte : EventArgs
    {
        public MessageByte(string content, byte[] picture)
        {
            Content = content;
            Picture = picture;
        }

        public string Content { get; set; }
        public  byte[] Picture { get; set; }
    }
}
