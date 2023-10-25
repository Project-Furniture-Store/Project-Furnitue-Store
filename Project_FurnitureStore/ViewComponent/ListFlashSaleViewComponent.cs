using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using Project_FurnitureStore.Models.OtherModel;
using System.Globalization;
using System.Threading.Tasks;

namespace Project_FurnitureStore.ViewComponents
{
    public class ListFlashSaleViewComponent : ViewComponent
    {

        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public ListFlashSaleViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }

        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ListFlashSale> flash = await GetFlashListAsync();
            List<SanPhamViewModel> sanpham = new List<SanPhamViewModel>();

            if (flash.Count > 0)
            {
                foreach (var sanPham in flash)
                {
                    var gioHienTai = DateTime.Now.TimeOfDay;
                    foreach (var khunggio in sanPham.KhungGio)
                    {
                        var gioBatDau = DateTime.ParseExact(khunggio, "H:mm", CultureInfo.InvariantCulture).TimeOfDay;

                        var gioKetThuc = gioBatDau.Add(TimeSpan.FromHours(1));

                        if (gioHienTai >= gioBatDau && gioHienTai <= gioKetThuc)
                        {
                            sanpham = await GetSanPhamListAsync();
                            break; 
                        }
                    }
                }
            }
          
            return View(sanpham);
        }

        public async Task<List<SanPhamViewModel>> GetSanPhamListAsync()
        {
            List<SanPhamViewModel> sanphamList = new List<SanPhamViewModel>();
          
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/FlashSale/GetFlashSaleFlashSale");

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

            return sanphamList;
        }


        public async Task<List<ListFlashSale>> GetFlashListAsync()
        {
            List<ListFlashSale> sanphamList = new List<ListFlashSale>();
            var gioHienTai = DateTime.Now.ToString("yyyy-MM-dd");
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/FlashSale/CheckSaleFlashSale?date={gioHienTai}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: loaihangList = await response.Content.ReadAsAsync<List<LoaiHangViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    sanphamList = JsonConvert.DeserializeObject<List<ListFlashSale>>(dataJson);
                }

            }

            return sanphamList;
        }

    }
}
