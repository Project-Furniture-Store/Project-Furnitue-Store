namespace FurnitureStore_API.Model.SanPham
{
    public class GetSanPhamResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<InsertSanPhamResquest> data { get; set; }

    }


}
