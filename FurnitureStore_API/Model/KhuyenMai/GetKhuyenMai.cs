namespace FurnitureStore_API.Model.KhuyenMai
{
    public class GetKhuyenMaiResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<InsertKhuyenMaiResquest> data { get; set; }
    }
}
