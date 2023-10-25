namespace Project_FurnitureStore.Models
{
    public class FlashSaleViewModel
    {
       
        public string? _id { get; set; }
        public int PhanTramGiam { get; set; }

        public List<string> SanPhamSale { get; set; }
        public List<string> KhungGio { get; set; }
        public string NgaySale { get; set; }
    }
}
