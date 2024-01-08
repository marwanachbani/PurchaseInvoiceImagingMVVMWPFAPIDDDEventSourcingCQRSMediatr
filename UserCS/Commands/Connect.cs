using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserCS.Commands
{
    public class Connect : BaseCommand
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
