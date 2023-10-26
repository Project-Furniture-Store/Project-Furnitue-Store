using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.DonHang;
using FurnitureStore_API.Model.SanPham;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DonHangController : ControllerBase
    {
        private readonly ICrudOperationDL_DonHang _crudOperationDL;

        // Constructor của controller, tiêm một đối tượng ICrudOperationDL để sử dụng
        public DonHangController(ICrudOperationDL_DonHang crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        [HttpGet]
        public async Task<ActionResult> GetBestProduct() //GetBestProduct is best-seller in the DONHANG collection
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetBestProduct();
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
        public async Task<ActionResult> GetAllDonHang() //GetBestProduct is best-seller in the DONHANG collection
        {
            // thông báo
            GetDonHangResponse response = new GetDonHangResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetAllDonHang();
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
        public async Task<ActionResult> GetSLProduct([FromQuery] string idsp) //Đếm sản phẩm trong đơn hàng có mã sp là X
        {
            // thông báo
            string response="0";

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetSLProduct(idsp);
            }
            catch (Exception ex)
            {// Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(response);
        }

    }
}
