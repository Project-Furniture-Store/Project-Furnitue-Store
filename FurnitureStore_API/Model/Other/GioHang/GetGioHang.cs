namespace FurnitureStore_API.Model.Other.GioHang
{
	public class GetGioHangResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }

		public List<InsertGioHangResquest> data { get; set; }
	}
}
