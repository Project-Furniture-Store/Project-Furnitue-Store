using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore_API.Model.LoaiHang
{
    public class InsertLoaiHangResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        [Required]
        public string TenLoai { get; set; }

        [Required]
        public string Anh { get; set; }


    }

    public class InsertLoaiHangResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

    }
}
