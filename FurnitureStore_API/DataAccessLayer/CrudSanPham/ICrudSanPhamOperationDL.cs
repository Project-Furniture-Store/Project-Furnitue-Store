
using FurnitureStore_API.Model.LoaiHang;
using FurnitureStore_API.Model.SanPham;

namespace FurnitureStore_API.DataAccessLayer
{
    public interface ICrudOperationDL_SanPham
    {
        public Task<GetSanPhamResponse> GetSanPham();
        public Task<GetSanPhamResponse> GetSanPhambycateId(string categoryId);
        public Task<GetSanPhamResponse> GetSanPhambyKeyword(string keyword);
        public Task<GetSanPhamResponse> GetSanPhambyId(string id);
        public Task<InsertSanPhamResponse> SetProduct(InsertSanPhamResquest Sanpham);
        public Task<InsertSanPhamResponse> UpdateProductbyID(InsertSanPhamResquest Sanpham);
        public Task<UpdateProductPatchResponse> UpdateProductbyIDPatch(UpdateProductPatchResquest Sanpham1);
    }
}
