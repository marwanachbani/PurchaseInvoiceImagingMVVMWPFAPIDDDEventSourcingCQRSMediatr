using Core;
using Data.MongoDb.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Data.MongoDb
{
    public class ImageDataHelper
    {
        public async Task<BaseResponse> AddImage(Guid id  , byte[] image)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("InvoiceImages");
            var imageCollection = database.GetCollection<BsonDocument>("DataImage");
            try
            {
                var imageDocument = new ImageDocument
                {
                    Id = id,
                    Data = image
                };

                await imageCollection.InsertOneAsync(imageDocument.ToBsonDocument());
                return new BaseResponse { Id = id, Response = "the image added succefully" , Successed = true };
            }
            catch (MongoException ex)
            {
                return new BaseResponse { Id = id, Response = ex.Message, Successed = false };
            }
            catch (Exception ex)
            {
                // Handle other exceptions here
                return new BaseResponse { Id = id, Response = ex.Message, Successed = false }; 
            }
        }
        public async Task<BaseResponse> UpdateImage(Guid id, byte[] image)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("InvoiceImages");
            var imageCollection = database.GetCollection<BsonDocument>("DataImage");

            try
            {
                // Check if the document with the given ID exists
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
                var existingDocument = await imageCollection.Find(filter).FirstOrDefaultAsync();

                if (existingDocument != null)
                {
                    // If the document exists, update its data
                    var update = Builders<BsonDocument>.Update.Set("Data", image);
                    await imageCollection.UpdateOneAsync(filter, update);

                    return new BaseResponse { Id = id, Response = "The image updated successfully", Successed = true };
                }
                else
                {
                    // If the document with the given ID does not exist, you may want to handle it accordingly
                    return new BaseResponse { Id = id, Response = "Image not found", Successed = false };
                }
            }
            catch (MongoException ex)
            {
                return new BaseResponse { Id = id, Response = ex.Message, Successed = false };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Id = id, Response = ex.Message, Successed = false };
            }
        }
        public async Task<BaseResponse> DeleteImage(Guid id)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("InvoiceImages");
            var imageCollection = database.GetCollection<BsonDocument>("DataImage");

            try
            {
                // Create a filter to find the document with the given ID
                var filter = Builders<BsonDocument>.Filter.Eq("_id", id);

                // Use DeleteOneAsync to delete the document matching the filter
                var result = await imageCollection.DeleteOneAsync(filter);

                if (result.DeletedCount > 0)
                {
                    return new BaseResponse { Id = id, Response = "The image deleted successfully", Successed = true };
                }
                else
                {
                    // If the document with the given ID does not exist, you may want to handle it accordingly
                    return new BaseResponse { Id = id, Response = "Image not found", Successed = false };
                }
            }
            catch (MongoException ex)
            {
                return new BaseResponse { Id = id, Response = ex.Message, Successed = false };
            }
            catch (Exception ex)
            {
                return new BaseResponse { Id = id, Response = ex.Message, Successed = false };
            }
        }
        public async Task<byte[]>GetImage(Guid id)
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase("InvoiceImages");
            var imageCollection = database.GetCollection<BsonDocument>("DataImage");
            var filter = Builders<BsonDocument>.Filter.Eq("_id", id); // imageId is the GUID you want to retrieve
            var result = await imageCollection.Find(filter).FirstOrDefaultAsync();

            if (result != null)
            {
                var imageData = result["Data"].AsBsonBinaryData.Bytes;
                return imageData;
            }
            return null;
        }
    }
}
