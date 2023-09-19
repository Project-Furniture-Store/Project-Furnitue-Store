
using FurnitureStore_API.Model.LoaiHang;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_LoaiHang
    {
        public Task<InsertLoaiHangResponse> InsertLoaiHang(InsertLoaiHangResquest request);
        public Task<GetLoaiHangResponse> GetLoaiHang();
        public Task<GetLoaiHangResponse> GetLoaiHangByID(string id);
    }
}
