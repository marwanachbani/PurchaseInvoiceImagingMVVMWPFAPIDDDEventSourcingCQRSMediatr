using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.SQLServer.Results
{
    public class BaseSqlResponse
    {
        public bool Successed { get; set; }
        public string Response { get; set; }
    }
}
