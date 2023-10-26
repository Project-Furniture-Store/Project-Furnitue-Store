using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FurnitureStore_API.Model.GioHang
{
    public class GioHangItemGH
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string TenKhachHang { get; set; }
        public int DonGia { get; set; }
        public string MauSac { get; set; }
        public int SoLuong { get; set; }
        public string KichThuoc { get; set; }
        public SanPham SanPham { get; set; }
    }

    public class SanPham
    {
        public string TenSP { get; set; }
        public string HinhAnh { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string IDSP { get; set; }
    }

}
