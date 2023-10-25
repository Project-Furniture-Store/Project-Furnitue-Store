
using FurnitureStore_API.Model.FlashSale;
using FurnitureStore_API.Model.KhachHang;
using FurnitureStore_API.Model.Other;
using FurnitureStore_API.Model.SanPham;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_FlashSale
    {
        public Task<GetListFlashSaleResponse> GetFlashSaleFlashSale();
        public Task<GetFlashSaleResponse> GetAllFlashSale();
        public Task<GetSanPhamResponse> GetProductFlashSale(string idFlashSale);
        public Task<GetFlashSaleResponse> GetFlashSale(string idfs);
        public Task<GetFlashSaleResponse> AddProductIDFs(string fsId, List<string> idSPs);
        public Task<GetFlashSaleResponse> DeleteProductIDFs(string fsId, List<string> idSPs);
    }
}
