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
            ViewBag.SearchTerm = search;
            return View(LoaiHangList);
        }





        [HttpGet]
        public async Task<IActionResult> DetailProduct(string idProduct)
        {
            
            List<SanPhamViewModel> SanPhamList = new List<SanPhamViewModel>();
            //lấy chi tiết sản phẩm
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/SanPham/GetSanPhambyId?id={idProduct}");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString();// Lấy phần "data" của JSON
                    SanPhamList = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(dataJson);
                }

            }
            //lấy mã loại - tên loại của sản phẩm đó
            string idloaiHang = SanPhamList[0].Loai;
           
            List<LoaiHangViewModel> LoaiHangList = new List<LoaiHangViewModel>();
            LoaiHangList = await GetLoaiHangbyid(idloaiHang);
            ViewData["MaLoai"] = LoaiHangList[0].id;
            ViewData["TenLoai"] = LoaiHangList[0].tenLoai;

            //Đếm sản phẩm trong đơn hàng
            ViewData["Count"] = await GetSLSPinDonHang(SanPhamList[0].id);

            return View(SanPhamList);

            
        }


        public async Task<List<LoaiHangViewModel>> GetLoaiHangbyid(string idloai)
        {
            List<LoaiHangViewModel> LoaiHangList = new List<LoaiHangViewModel>();
            HttpResponseMessage response1 = await _client.GetAsync(_client.BaseAddress + $"/LoaiHang/GetLoaiHangByID?id={idloai}");
            if (response1.IsSuccessStatusCode)
            {
                string data = await response1.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString();// Lấy phần "data" của JSON
                    LoaiHangList = JsonConvert.DeserializeObject<List<LoaiHangViewModel>>(dataJson);
                }

            }
            return LoaiHangList;
        }


        public async Task<string> GetSLSPinDonHang(string idsp)
        {
            string result = "0";
            HttpResponseMessage response1 = await _client.GetAsync(_client.BaseAddress + $"/DonHang/GetSLProduct?idsp={idsp}");

            if (response1.IsSuccessStatusCode)
            {
                string data = await response1.Content.ReadAsStringAsync();


                result = data;


            }

            return result;
        }


    }
}
