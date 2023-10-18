using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore_API.Model.SanPham
{
    public class UpdateProductPatchResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        public string TenSP { get; set; }

        public string MieuTa { get; set; }

        public double DonGia { get; set; }

        public List<string>? MauSac { get; set; }
        public string KichCo { get; set; }
        public string Hinh { get; set; }


        [BsonElement("Supplier")]
        public Supplierr NhaCungCap { get; set; }
        
        public int GoldCoin { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? Loai { get; set; }
    }

    public class Supplierr
    {
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public double DonGiaCC { get; set; }
    }


    public class UpdateProductPatchResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
