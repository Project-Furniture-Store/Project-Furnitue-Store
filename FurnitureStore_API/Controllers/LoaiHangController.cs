using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.LoaiHang;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore_API.Controllers
{
    // Định nghĩa route cho tất cả các phương thức trong controller và đánh dấu controller là ApiController
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class LoaiHangController : ControllerBase
    {
    

        private readonly ICrudOperationDL_LoaiHang _crudOperationDL;

        // Constructor của controller, tiêm một đối tượng ICrudOperationDL để sử dụng
        public LoaiHangController(ICrudOperationDL_LoaiHang crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        // Xử lý yêu cầu HTTP POST tới endpoint '/api/CrudOperation/InsertRecord'
        [HttpPost]
        public async Task<ActionResult> InsertLoaiHang(InsertLoaiHangResquest request)
        {
            InsertLoaiHangResponse response = new InsertLoaiHangResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.InsertLoaiHang(request);
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
        public async Task<ActionResult> GetLoaiHang()
        {
            // thông báo
            GetLoaiHangResponse response = new GetLoaiHangResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetLoaiHang();
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
        public async Task<ActionResult> GetLoaiHangByID([FromQuery] string id)
        {
            // thông báo
            GetLoaiHangResponse response = new GetLoaiHangResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetLoaiHangByID(id);
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
        public async Task<ActionResult> GetLoaiHangByName([FromQuery] string name)
        {
            // thông báo
            GetLoaiHangResponse response = new GetLoaiHangResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetLoaiHangByName(name);
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
