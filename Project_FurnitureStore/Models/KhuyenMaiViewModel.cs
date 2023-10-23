namespace Project_FurnitureStore.Models
{
    public class KhuyenMaiViewModel
    {
        public string _id { get; set; }
        public string TenKhuyenMai { get; set; }
        public string Hinh { get; set; }
        public string NgayKhuyenMai { get; set; }
        public string NgayKetThuc { get; set; }
        public string DieuKien { get; set; }
        public string TienGiam { get; set; }
        public List<string> IdSP { get; set; }
    }
}
