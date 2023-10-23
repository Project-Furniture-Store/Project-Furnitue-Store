using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.KhuyenMai;
using FurnitureStore_API.Model.SanPham;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore_API.Controllers
{

    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class KhuyenMaiController : ControllerBase
    {
        private readonly ICrudOperationDL_KhuyenMai _crudOperationDL;

        // Constructor của controller, tiêm một đối tượng ICrudOperationDL để sử dụng
        public KhuyenMaiController(ICrudOperationDL_KhuyenMai crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }


        [HttpGet]
        public async Task<ActionResult> GetKhuyenMai()
        {
            // thông báo
            GetKhuyenMaiResponse response = new GetKhuyenMaiResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetKhuyenMai();
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
        public async Task<ActionResult> ListProductKhuyenMai(string idKhuyenMai)
        {
            // thông báo
            GetSanPhamResponse response = new GetSanPhamResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.ListProductKhuyenMai(idKhuyenMai);
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
        public async Task<ActionResult> GetKhuyenMaibyId(string idKhuyenMai)
        {
            // thông báo
            GetKhuyenMaiResponse response = new GetKhuyenMaiResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetKhuyenMaibyId(idKhuyenMai);
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
		public async Task<ActionResult> AddProductID(string khuyenMaiId, List<string> idSPs)
		{
			// thông báo
			GetKhuyenMaiResponse response = new GetKhuyenMaiResponse();

			try
			{
				// Gọi phương thức InsertRecord của đối tượng _crudOperationDL
				response = await _crudOperationDL.AddProductID(khuyenMaiId, idSPs);
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
