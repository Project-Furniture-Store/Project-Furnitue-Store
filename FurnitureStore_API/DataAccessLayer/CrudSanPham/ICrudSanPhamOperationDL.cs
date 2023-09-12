
using FurnitureStore_API.Model.LoaiHang;
using FurnitureStore_API.Model.SanPham;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_SanPham
    {
        public Task<GetSanPhamResponse> GetSanPham();
    }
}
