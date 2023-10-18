using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using Project_FurnitureStore.Models.OtherModel;
using System.Threading.Tasks;

namespace Project_FurnitureStore.ViewComponents
{
    public class ListProductAllViewComponent  : ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public ListProductAllViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }


        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<SanPhamViewModel> sanphamList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/SanPham/GetSanPham");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: loaihangList = await response.Content.ReadAsAsync<List<LoaiHangViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    sanphamList = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(dataJson);
                }

            }
            await loadLoaiHang();

            return View(sanphamList);
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
    }
}
