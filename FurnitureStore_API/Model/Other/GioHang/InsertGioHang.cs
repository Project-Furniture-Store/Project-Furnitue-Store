using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace FurnitureStore_API.Model.Other.GioHang
{
	public class InsertGioHangResquest
    {
            [BsonRepresentation(BsonType.ObjectId)]
            public string SanPhamCart { get; set; }
            public int DonGia { get; set; }
            public string MauSac { get; set; }
            public int SoLuong { get; set; }
            public string KichCo { get; set; }
    }

    public class InsertGioHangResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
    }
}
