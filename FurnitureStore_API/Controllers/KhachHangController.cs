﻿using FurnitureStore_API.DataAccessLayer;
using FurnitureStore_API.Model.KhachHang;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureStore_API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly ICrudOperationDL_KhachHang _crudOperationDL;

        // Constructor của controller, tiêm một đối tượng ICrudOperationDL để sử dụng
        public KhachHangController(ICrudOperationDL_KhachHang crudOperationDL)
        {
            _crudOperationDL = crudOperationDL;
        }

        [HttpGet]
        public async Task<ActionResult> GetKhachHangLogin([FromQuery] string account,string pass)
        {
            // thông báo
            GetKhachHangResponse response = new GetKhachHangResponse();

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                response = await _crudOperationDL.GetKhachHangByID(account, pass);
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
        public async Task<ActionResult> GetSLSanPham([FromQuery] string idKH)
        {
            // thông báo
            String soluong="0";

            try
            {
                // Gọi phương thức InsertRecord của đối tượng _crudOperationDL
                soluong = await _crudOperationDL.GetSLSanPham(idKH);
            }
            catch (Exception ex)
            {
                
                soluong = "Error" + ex.Message;
            }

            // Trả về phản hồi HTTP với kết quả từ _crudOperationDL
            return Ok(soluong);
        }

    }
}