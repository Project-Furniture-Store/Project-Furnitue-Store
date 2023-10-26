namespace Project_FurnitureStore.Models
{
    public class GioHangViewModel
    {
        
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
        public string IDSP { get; set; }
    }
}