namespace Project_FurnitureStore.Models.OtherModel
{
    public class ListFlashSale
    {
        public string id { get; set; }
        public int PhanTramGiam { get; set; }
        public List<string> GioSale { get; set; }
        public SanPhamInfoModel SanPhamInfo { get; set; }
    }

    public class SanPhamInfoModel
    {
        public string id { get; set; }
        public string TenSP { get; set; }
        public string MieuTa { get; set; }
        public double DonGia { get; set; }
        public List<string> MauSac { get; set; }
        public string KichCo { get; set; }
        public string Hinh { get; set; }
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
        public string KhachHangFeeback { get; set; }
        public int Rate { get; set; }
        public string ChiTiet { get; set; }
        public string Hinh { get; set; }
        public string Video { get; set; }
    }
}
