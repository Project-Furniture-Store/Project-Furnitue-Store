using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using Project_FurnitureStore.Models.OtherModel;
using System.Threading.Tasks;


namespace Project_FurnitureStore.ViewComponents
{
	public class ListVoucherViewComponent:ViewComponent
	{
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public ListVoucherViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }



        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<KhuyenMaiViewModel> khuyenmailist = new List<KhuyenMaiViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/KhuyenMai/GetKhuyenMai");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    khuyenmailist = JsonConvert.DeserializeObject<List<KhuyenMaiViewModel>>(dataJson);

                   
                }
            }

            return View(khuyenmailist);
        }


    }
}
