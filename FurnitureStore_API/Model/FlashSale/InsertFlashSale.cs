using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FurnitureStore_API.Model.FlashSale
{
	public class InsertFlashSaleResquest
	{
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public int PhanTramGiam { get; set; }
        public List<ObjectId> SanPhamSale { get; set; }
        public List<string> KhungGio { get; set; }
        public string Ngay { get; set; }
    }


    public class InsertFlashSaleResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
