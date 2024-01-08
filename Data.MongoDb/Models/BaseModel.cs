using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.MongoDb.Models
{
    public class BaseModel
    {
        [BsonRepresentation(MongoDB.Bson.BsonType.Binary)]
        public Guid Id { get; set; }
   
    }
}
