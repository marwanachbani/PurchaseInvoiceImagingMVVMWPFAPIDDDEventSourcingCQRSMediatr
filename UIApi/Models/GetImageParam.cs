using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIApi.Models
{
    public class GetImageParam
    {
        public string Id { get; set; }

        public GetImageParam(string id)
        {
            Id = id;
        }
    }
}
