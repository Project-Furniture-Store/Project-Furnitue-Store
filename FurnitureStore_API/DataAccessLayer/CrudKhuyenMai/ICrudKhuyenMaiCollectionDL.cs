using FurnitureStore_API.Model.KhuyenMai;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_KhuyenMai
    {
        public  Task<GetKhuyenMaiResponse> GetKhuyenMai();
    }
}
