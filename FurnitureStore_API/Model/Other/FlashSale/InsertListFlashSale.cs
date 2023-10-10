using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FurnitureStore_API.Model.Other
{
	
    public class InsertListFlashSaleResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? id { get; set; }
        public int PhanTramGiam { get; set; }
        public List<string> GioSale { get; set; }
        public SanPhamInfoModel SanPhamInfo { get; set; }
    }

    public class SanPhamInfoModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }
        public string TenSP { get; set; }
        public string MieuTa { get; set; }
        public string DonGia { get; set; }
        public List<string> MauSac { get; set; }
        public string KichCo { get; set; }
        public string Hinh { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string KhuyenMai { get; set; }
        public NhaCungCapModel NhaCungCap { get; set; }
        public DanhGiaModel DanhGia { get; set; }
        public int GoldCoin { get; set; }
    }

    public class NhaCungCapModel
    {
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string DonGiaCC { get; set; }
    }

    public class DanhGiaModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string KhachHangFeeback { get; set; } 
        public int Rate { get; set; }
        public string ChiTiet { get; set; }
        public string Hinh { get; set; }
        public string Video { get; set; }
    }

    public class InsertListFlashSaleResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }

}
