using FurnitureStore_API.Model;
using FurnitureStore_API.Model.KhachHang;
using FurnitureStore_API.Model.Other.GioHang;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_KhachHang
    {
        public Task<GetKhachHangResponse> GetKhachHangByID(string account, string pass);
        public Task<string> GetSLSanPham(string idKH);
        public Task<GetKhachHangResponse> AddProductCart(string idkh, string idsp, string mausac, string dongia, string sl, string size);
        public Task<GetKhachHangResponse> GetKhachHangbyID(string idKH);
    }
}
