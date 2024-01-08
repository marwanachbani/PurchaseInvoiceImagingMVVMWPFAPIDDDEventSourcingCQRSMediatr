using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MongoDb.Models
{
    public class ImageDocument : BaseModel
    {
        
        public byte[] Data { get; set; }
    }
}
