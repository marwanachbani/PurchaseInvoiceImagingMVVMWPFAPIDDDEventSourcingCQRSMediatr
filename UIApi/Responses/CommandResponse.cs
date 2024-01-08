using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Responses
{
    public class CommandResponse 
    {
        public CommandResponse(bool successed, string respnse)
        {
            Successed = successed;
            Respnse = respnse;
        }

        public bool Successed { get; set; }
        public string Respnse { get; set; }
    }
}
