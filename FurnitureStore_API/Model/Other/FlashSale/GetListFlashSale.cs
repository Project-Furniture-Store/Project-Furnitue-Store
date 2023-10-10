namespace FurnitureStore_API.Model.Other
{
	public class GetListFlashSaleResponse
	{
		public bool IsSuccess { get; set; }
		public string Message { get; set; }

		public List<InsertListFlashSaleResquest> data { get; set; }
	}
}
