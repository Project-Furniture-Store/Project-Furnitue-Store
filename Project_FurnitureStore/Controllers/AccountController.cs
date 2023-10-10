using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Project_FurnitureStore.Controllers
{
    public class AccountController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
        }

        
        public IActionResult Login(string currentUrl, string ?id,string ?mausac, string ?dongia, string ?sl, string ?size)
        {
            if(id!=null)
            {
                string infor = id +'/'+ mausac + '/' + dongia + '/' + sl + '/' + size+'/'+ currentUrl;
                HttpContext.Session.SetString("inforCart", infor);
            }

            HttpContext.Session.SetString("returnCurrentUrl", currentUrl);
            return View();
        }




        [HttpPost]
        public async Task<ActionResult> Login(string account1, string pass1)
        {
           

            List<KhachHangViewModel> KhachHangList = new List<KhachHangViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhachHang/GetKhachHangLogin?account={account1}&pass={pass1}");
            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhachHangList = await response.Content.ReadAsAsync<List<KhachHangViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    KhachHangList = JsonConvert.DeserializeObject<List<KhachHangViewModel>>(dataJson);
                    if (KhachHangList.Any())
                    {
                        HttpContext.Session.SetString("IDCustomer", value: KhachHangList[0]._id.ToString());
                        string url = HttpContext.Session.GetString("returnCurrentUrl");
                        GetSLSanPham();
                        var inforProduct = HttpContext.Session.GetString("inforCart");
                        if (inforProduct != null)
                        {
                            string[] array = inforProduct.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                            return RedirectToAction("ThemGioHang", "Cart", new { idsp = array[0], mausac = array[1], dongia = array[2], sl = array[3], size = array[4], url = array[5] });
                        }
                        return Redirect(url);
                    }
                    else
                    {
                        ViewBag.UserName = "Tài khoản hoặc mật khẩu sai";
                        return View();
                    }
                }
               

            }
            return View();
        }


        public async Task GetSLSanPham()
        {
            string sl = "0";
            string IdKH = HttpContext.Session.GetString("IDCustomer");

            if (!string.IsNullOrEmpty(IdKH))
            {
                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhachHang/GetSLSanPham?idKH={IdKH}");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    sl = data;
                    HttpContext.Session.SetString("SoLuongSanPham", sl);
                }
            }
        }
    }
}
