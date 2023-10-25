using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;



namespace FurnitureStore_API.Model.FlashSale
{
    public class UpdataFlashSaleRequest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public int PhanTramGiam { get; set; }
        public List<string> KhungGio { get; set; }
        public string NgaySale { get; set; }
    }


    public class UpdataFlashSaleResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
