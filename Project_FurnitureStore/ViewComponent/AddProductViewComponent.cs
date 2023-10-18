using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using System.Threading.Tasks;

namespace Project_FurnitureStore.ViewComponents
{
    public class AddProductViewComponent: ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public AddProductViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

       
        public async Task<IViewComponentResult> InvokeAsync()
        {
            await loadKhuyenMai();
            await loadLoaiHang();
            return View();
        }

        public async Task<IViewComponentResult> AddProduct(string tensp, string mieutta, int dongia, string mausac, string kichco, string hinh, string khuyenmai, string nhacc, string dcncc, string sdt, string loai)
        {
          
            return View();
        }




        public async Task loadKhuyenMai()
        {  List<KhuyenMaiViewModel> KhuyenMaiList = new List<KhuyenMaiViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/KhuyenMai/GetKhuyenMai");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    KhuyenMaiList = JsonConvert.DeserializeObject<List<KhuyenMaiViewModel>>(dataJson);
                }

            }
            TempData["KhuyenMailist"] = KhuyenMaiList;
           
        }

        public async Task loadLoaiHang()
        {
            List<LoaiHangViewModel> LoaiHangList = new List<LoaiHangViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/LoaiHang/GetLoaiHang");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    LoaiHangList = JsonConvert.DeserializeObject<List<LoaiHangViewModel>>(dataJson);
                }

            }
            TempData["LoaiHanglist"] = LoaiHangList;

        }

        //public IViewComponentResult Invoke(string Tensp, int dongia, string mausac, string mieuta, string hinh, string kichthuoc, string khuyenmai, string nhacc, string diachi, string phone, string loai)
        //{
        //    return View();
        //}


    }
}
