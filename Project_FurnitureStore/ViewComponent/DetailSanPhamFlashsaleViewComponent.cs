using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using System.Threading.Tasks;

namespace Project_FurnitureStore.ViewComponents
{
    public class DetailSanPhamFlashsaleViewComponent : ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public DetailSanPhamFlashsaleViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }
      
        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync(string idflashsale)
        {
            List<SanPhamViewModel> sanphamList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/FlashSale/GetProductFlashSale?idFlashSale={idflashsale}");

            if (response.IsSuccessStatusCode)
            {
                
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    sanphamList = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(dataJson);
                }

            }

            return View(sanphamList);
        }
    }
}
