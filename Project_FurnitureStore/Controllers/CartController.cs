using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace Project_FurnitureStore.Controllers
{
    public class CartController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public CartController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }


        
        public async Task<ActionResult> ThemGioHang(string idsp, string mausac, string dongia, string sl, string size, string url)
        {
            var idKHh= HttpContext.Session.GetString("IDCustomer");
            List<LoaiHangViewModel> LoaiHangList = new List<LoaiHangViewModel>();
       
            HttpResponseMessage response1 = await _client.GetAsync(_client.BaseAddress + $"/KhachHang/AddProductCart?idkh={idKHh}&idsp={idsp}&mausac={mausac}&dongia={dongia}&sl={sl}&size={size}");
            if (response1.IsSuccessStatusCode)
            {
                string data = await response1.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data);

                if (jsonObject != null && jsonObject["isSuccess"] != null)
                {
                    bool isSuccess = jsonObject.Value<bool>("isSuccess");

                    if (isSuccess)
                    {
                        return Redirect(url);
                    }
                    else
                    {
                        string message = jsonObject.Value<string>("message");
                        // Xử lý khi có thông báo lỗi từ API (nếu có)
                    }
                }
                else
                {
                    // Xử lý khi dữ liệu JSON không đúng định dạng
                }
            }
            else
            {
               
            }
            return View();
        }



    }
}
