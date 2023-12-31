﻿using System.ComponentModel.DataAnnotations;

namespace Project_FurnitureStore.Models
{
    public class SanPhamViewModel
    {
        public string? id { get; set; }

        public string? TenSP { get; set; }

        public string MieuTa { get; set; }

        public double DonGia { get; set; }

        public List<string>? MauSac { get; set; }
        public string? KichCo { get; set; }
        public string? Hinh { get; set; }



        public Supplier? NhaCungCap { get; set; }
        public List<Rating> DanhGia { get; set; }
        
        public int? GoldCoin { get; set; }

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
        public string? KhachHangFeeback { get; set; } // Thêm trường này vào class Rating
        public int? Rate { get; set; }
        public string? ChiTiet { get; set; }
        public string? Hinh { get; set; }
        public string? Video { get; set; }
    }

}
