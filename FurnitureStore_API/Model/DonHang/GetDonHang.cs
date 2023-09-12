namespace FurnitureStore_API.Model.DonHang
{
    public class GetDonHangResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<InsertDonHangResquest> data { get; set; }
    }
}
