using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore_API.Model.KhuyenMai
{
	public class UpdataKhuyenMaiPatchResquest
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
    }


    public class UpdataKhuyenMaiPatchResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }


}
