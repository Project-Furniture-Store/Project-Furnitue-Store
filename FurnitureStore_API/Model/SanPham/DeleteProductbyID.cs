using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore_API.Model.SanPham
{
    public class DeleteProductbyIDResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
    }

    public class DeleteProductbyIDResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
