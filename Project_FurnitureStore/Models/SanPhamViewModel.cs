using System.ComponentModel.DataAnnotations;

namespace Project_FurnitureStore.Models
{
    public class SanPhamViewModel
    {
        public string id { get; set; }
        [Required]
        public string TenSP { get; set; }
        [Required]
        public string MieuTa { get; set; }
        [Required]
        public double DonGia { get; set; }

        public List<String> MauSac { get; set; }
        public string KichCo { get; set; }
        public string Hinh { get; set; }

        public string KhuyenMai { get; set; } //idkm

        public Supplier NhaCungCap { get; set; }
        public Rating DanhGia { get; set; }
        public int GoldCoin { get; set; }
        public string Loai { get; set; }
    }



    public class Supplier
    {
        public string TenNCC { get; set; }
        public string DiaChi { get; set; }
        public string SDT { get; set; }
        public double DonGiaCC { get; set; }
    }

    public class Rating
    {
        public string KhachHangFeeback { get; set; } // Thêm trường này vào class Rating
        public int Rate { get; set; }
        public string ChiTiet { get; set; }
        public string Hinh { get; set; }
        public string Video { get; set; }
    }

}
