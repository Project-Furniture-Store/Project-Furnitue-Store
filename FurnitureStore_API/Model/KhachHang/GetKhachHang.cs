namespace FurnitureStore_API.Model.KhachHang
{
    public class GetKhachHangResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<InsertKhachHangResquest> data { get; set; }
    }
}
