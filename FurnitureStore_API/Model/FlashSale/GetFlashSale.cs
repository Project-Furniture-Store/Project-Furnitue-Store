namespace FurnitureStore_API.Model.FlashSale
{
    public class GetFlashSaleResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        public List<InsertFlashSaleResquest> data { get; set; }
    }
}
