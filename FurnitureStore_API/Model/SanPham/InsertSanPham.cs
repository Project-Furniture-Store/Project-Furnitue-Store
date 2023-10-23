using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace FurnitureStore_API.Model.SanPham
{
    public class InsertSanPhamResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }

        public string? TenSP { get; set; }

        public string MieuTa { get; set; }

        public double DonGia { get; set; }

        public List<string>? MauSac { get; set; }
        public string? KichCo { get; set; }
        public string? Hinh { get; set; }

        

        public Supplier? NhaCungCap { get; set; }
        public List<Rating> DanhGia { get; set; }
        public InsertSanPhamResquest()
        {
            DanhGia = new List<Rating>();
        }
        public int? GoldCoin { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? Loai { get; set; }

    }

    public class Supplier
    {
        public string? TenNCC { get; set; }
        public string? DiaChi { get; set; }
        public string? SDT { get; set; }
        public double? DonGiaCC { get; set; }
    }

    public class Rating
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? KhachHangFeeback { get; set; } // Thêm trường này vào class Rating
        public int? Rate { get; set; }
        public string? ChiTiet { get; set; }
        public string? Hinh { get; set; }
        public string? Video { get; set; }
    }


    public class InsertSanPhamResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}