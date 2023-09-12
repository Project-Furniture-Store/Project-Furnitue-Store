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
    public class HomeController : Controller
    {


     
        Uri baseAddress=new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public HomeController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;
           
        }

        public IActionResult Index()
        {
            return View();
        }


    
        //[HttpGet]
        //public async Task<IActionResult> getItemListing()
        //{
        //    List<LoaiHangViewModel> loaihangList = new List<LoaiHangViewModel>();
        //    HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/LoaiHang/GetLoaiHang");

        //    if (response.IsSuccessStatusCode)
        //    {
        //        // Xử lý dữ liệu từ response ở đây (ví dụ: loaihangList = await response.Content.ReadAsAsync<List<LoaiHangViewModel>>();)
        //        string data = await response.Content.ReadAsStringAsync();
        //        var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

        //        if (jsonObject != null && jsonObject["data"] != null)
        //        {
        //            var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
        //            loaihangList = JsonConvert.DeserializeObject<List<LoaiHangViewModel>>(dataJson);
        //        }

        //    }

        //    return PartialView(loaihangList);
        //}



















        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        //public IActionResult Index()
        //{
        //    return View();
        //}

        //public IActionResult Privacy()
        //{
        //    return View();
        //}

        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
    }
}