namespace Project_FurnitureStore.Models
{
	public class KhachHangViewModel
	{
        public string _id { get; set; }
        public string TenKhachHang { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public string DiemTichLuy { get; set; }
        public List<GioHangItem> GioHang { get; set; }
    }

    public class GioHangItem
    {
        public string SanPhamCart { get; set; }
        public int DonGia { get; set; }
        public string MauSac { get; set; }
        public int SoLuong { get; set; }
        public string KichCo { get; set; }
    }
}
