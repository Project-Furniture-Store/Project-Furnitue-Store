namespace FurnitureStore_API.Model.LoaiHang
{
    public class GetLoaiHangResponse
    {
       public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<InsertLoaiHangResquest> data { get; set; }

    }
}
