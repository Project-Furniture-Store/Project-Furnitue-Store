using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using System.Threading.Tasks;


namespace Project_FurnitureStore.ViewComponents
{
    public class PersonalViewComponent:ViewComponent
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public PersonalViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }


        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string idKhh = HttpContext.Session.GetString("IDCustomer");
            List<KhachHangViewModel> KhachHangList = new List<KhachHangViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhachHang/GetKhachHangbyID?idKH={idKhh}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); 

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString();
                    KhachHangList = JsonConvert.DeserializeObject<List<KhachHangViewModel>>(dataJson);
                }

            }

            return View(KhachHangList);
        }

    }
}
