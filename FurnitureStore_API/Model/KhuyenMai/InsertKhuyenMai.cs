using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FurnitureStore_API.Model.KhuyenMai
{
    public class InsertKhuyenMaiResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string TenKhuyenMai { get; set; }
        public string Hinh { get; set; }
        public string NgayKhuyenMai { get; set; }
        public string NgayKetThuc { get; set; }
        public string DieuKien { get; set; }
        public string TienGiam { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public List<string> IdSP { get; set; }
    }


    public class InsertKhuyenMaiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}
