using FurnitureStore_API.Model.KhuyenMai;
using FurnitureStore_API.Model.SanPham;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_KhuyenMai
    {
        public  Task<GetKhuyenMaiResponse> GetKhuyenMai();
        public Task<GetKhuyenMaiResponse> GetKhuyenMaibyId(string idKhuyenMai);
		public Task<GetSanPhamResponse> ListProductKhuyenMai(string idKhuyenMai);
		public Task<GetKhuyenMaiResponse> AddProductID(string khuyenMaiId, List<string> idSPs);
        public Task<GetKhuyenMaiResponse> DelelteProductKMID(string khuyenMaiId, List<string> idSPs);
        public Task<InsertKhuyenMaiResponse> SetKhuyenMai(InsertKhuyenMaiResquest khuyenmai);
        public Task<UpdataKhuyenMaiPatchResponse> UpdateKhuyenMaibyIDPatch(UpdataKhuyenMaiPatchResquest Khuyenmai);
        public Task<UpdataKhuyenMaiPatchResponse> DeleteKhuyenMaibyID(string idkhuyenmai);

    }
}
