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
    public class ProductController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public async Task<IActionResult> ProductByCategory (string categoryId)// Get all product by category identify
        {

            List<LoaiHangViewModel> LoaiHangList = new List<LoaiHangViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/LoaiHang/GetLoaiHangByID?id={categoryId}");
            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: loaihangList = await response.Content.ReadAsAsync<List<LoaiHangViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    LoaiHangList = JsonConvert.DeserializeObject<List<LoaiHangViewModel>>(dataJson);
                }

            }
            return View(LoaiHangList);
        }


        [HttpGet]
        public async Task<IActionResult> SearchProduct(string search)
        {

            List<SanPhamViewModel> LoaiHangList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/SanPham/GetSanPhambyKeyword?keyword={search}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString();// Lấy phần "data" của JSON
                    LoaiHangList = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(dataJson);
                }

            }
            return View(LoaiHangList);
        }




    }
}
