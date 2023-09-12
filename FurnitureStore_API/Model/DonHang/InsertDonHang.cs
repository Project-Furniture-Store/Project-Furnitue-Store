using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FurnitureStore_API.Model.DonHang
{
    public class InsertDonHangResquest
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? _id { get; set; }
        public string KhachHang { get; set; }
        public int TongTien { get; set; }
        public DateTime NgayDat { get; set; }
        public string TrangThai { get; set; }

        public List<ChiTietDonHang> ChiTietDonHang { get; set; }

    }


    public class ChiTietDonHang
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string SanPhamDH { get; set; }
        public int Sluong { get; set; }
        public string MauSac { get; set; }
        public string KichCo { get; set; }
        public int DonGia { get; set; }
    }

    public class InsertDonHangResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
