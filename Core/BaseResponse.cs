using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class BaseResponse
    {
        public Guid Id { get; set; }
        public bool Successed { get; set; }
        public string Response { get; set; }
    }
}
