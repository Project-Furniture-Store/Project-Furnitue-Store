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
            List<ListFlashSale> sanphamList = await GetSanPhamListAsync();

            var gioHienTai = DateTime.Now; // Giờ hiện tại

            var filteredSanPham = new List<ListFlashSale>();

            foreach (var sanPham in sanphamList)
            {
                foreach (var gioSale in sanPham.GioSale)
                {
                    DateTime start = DateTime.ParseExact(gioSale, "HH:mm", CultureInfo.InvariantCulture);

                    DateTime end = start.AddHours(1);

                    if (gioHienTai >= start && gioHienTai <= end)
                    {
                        filteredSanPham.Add(sanPham);
                        break; // Thoát khỏi vòng lặp inner khi tìm thấy giờ trùng khớp
                    }
                }
            }
            return View(filteredSanPham);
        }

        public async Task<List<ListFlashSale>> GetSanPhamListAsync()
        {
            List<ListFlashSale> sanphamList = new List<ListFlashSale>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/FlashSale/GetFlashSaleFlashSale");

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
