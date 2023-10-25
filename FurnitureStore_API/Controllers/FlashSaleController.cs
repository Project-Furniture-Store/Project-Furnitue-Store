using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.FlashSale;
using FurnitureStore_API.Model.Other;
using FurnitureStore_API.Model.SanPham;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FlashSaleController : ControllerBase
    {
        private readonly ICrudOperationDL_FlashSale _crudOperationDL;

        // Constructor của controller, tiêm một đối tượng ICrudOperationDL để sử dụng
        public FlashSaleController(ICrudOperationDL_FlashSale crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }


        [HttpGet]
        public async Task<ActionResult> GetFlashSaleFlashSale() // get all product in data
        {
            // thông báo
            GetListFlashSaleResponse response = new GetListFlashSaleResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetFlashSaleFlashSale();
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
        public async Task<ActionResult> GetAllFlashSale() // get all product in data
        {
            // thông báo
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetAllFlashSale();
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
        public async Task<ActionResult> AddProductIDFs(string fsId, List<string> idSPs)
        {
            // thông báo
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.AddProductIDFs(fsId, idSPs);
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
        public async Task<ActionResult> DeleteProductIDFs(string fsId, List<string> idSPs)
        {
            // thông báo
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.DeleteProductIDFs(fsId, idSPs);
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
        public async Task<ActionResult> GetFlashSale(string idfs) // get all product in data
        {
            // thông báo
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetFlashSale(idfs);
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
        public async Task<ActionResult> GetProductFlashSale(string idFlashSale) // get all product in data
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetProductFlashSale(idFlashSale);
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
