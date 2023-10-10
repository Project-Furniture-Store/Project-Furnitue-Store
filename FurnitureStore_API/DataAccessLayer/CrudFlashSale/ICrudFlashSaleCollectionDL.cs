
using FurnitureStore_API.Model.KhachHang;
using FurnitureStore_API.Model.Other;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_FlashSale
    {
        public Task<GetListFlashSaleResponse> GetFlashSaleFlashSale();
    }
}
