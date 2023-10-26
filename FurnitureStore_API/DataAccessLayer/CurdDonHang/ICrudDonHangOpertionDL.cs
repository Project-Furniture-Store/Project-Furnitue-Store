using FurnitureStore_API.Model.DonHang;
using FurnitureStore_API.Model.SanPham;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_DonHang
    {
        public Task<GetSanPhamResponse> GetBestProduct();
        public Task<String> GetSLProduct(string idsp);
        public  Task<GetDonHangResponse> GetAllDonHang();
    }
}
