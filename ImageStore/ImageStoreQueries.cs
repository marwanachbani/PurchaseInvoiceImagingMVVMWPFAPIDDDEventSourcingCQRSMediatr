using Data.MongoDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageStore
{
    public class ImageStoreQueries
    {
        private  ImageDataHelper imageDataHelper = new();
        public  async Task<byte[]>GetImage(Guid guid)
        {
            var result = await imageDataHelper.GetImage(guid);
            return result;
        }
    }
}
