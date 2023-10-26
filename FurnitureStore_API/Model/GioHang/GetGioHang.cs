namespace FurnitureStore_API.Model.GioHang
{
    public class GetGioHang
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<GioHangItemGH> data { get; set; }
    }
}
