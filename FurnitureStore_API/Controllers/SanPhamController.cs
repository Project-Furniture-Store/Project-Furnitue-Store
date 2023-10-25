using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.SanPham;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SanPhamController : ControllerBase
    {
        private readonly ICrudOperationDL_SanPham _crudOperationDL;

        // Constructor của controller, tiêm một đối tượng ICrudOperationDL để sử dụng
        public SanPhamController(ICrudOperationDL_SanPham crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        [HttpGet]
        public async Task<ActionResult> GetSanPham() // get all product in data
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSanPham();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult> GetSanPhamNoKhuyenMai() // get all product in data
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSanPhamNoKhuyenMai();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult> GetSanPhamNoFlashSale() // get all product in data
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSanPhamNoFlashSale();
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult> GetSanPhambycateId([FromQuery] string categoryId) // get all product by category id
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSanPhambycateId(categoryId);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }


        [HttpGet]
        public async Task<ActionResult> GetSanPhambyKeyword([FromQuery] string keyword) // get all product by key word
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSanPhambyKeyword(keyword);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }






        [HttpGet]
        public async Task<ActionResult> GetSanPhambyId([FromQuery] string id) // get product by id
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSanPhambyId(id);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult> SetProduct(InsertSanPhamResquest Sanpham) // get product by id
        {
            // thông báo
            InsertSanPhamResponse response = new InsertSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.SetProduct(Sanpham);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }





        [HttpPatch]
        public async Task<ActionResult> UpdateProductbyIDPatch(UpdateProductPatchResquest Sanpham) // get product by id
        {
            // thông báo
            UpdateProductPatchResponse response = new UpdateProductPatchResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.UpdateProductbyIDPatch(Sanpham);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteProductbyID(string id) // get product by id
        {
            // thông báo
            DeleteProductbyIDResponse response = new DeleteProductbyIDResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.DeleteProductbyID(id);
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }
    }
}
