using FurnitureStore_API.Model;
using FurnitureStore_API.Model.KhachHang;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_KhachHang
    {
        public Task<GetKhachHangResponse> GetKhachHangByID(string account, string pass);
        public Task<string> GetSLSanPham(string idKH);
    }
}
