using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.Other;
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


    }
}
