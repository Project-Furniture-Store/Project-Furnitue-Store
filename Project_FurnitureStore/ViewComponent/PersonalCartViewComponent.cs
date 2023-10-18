using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;


namespace Project_FurnitureStore.ViewComponents
{
    public class PersonalCartViewComponent:ViewComponent
    {

        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public PersonalCartViewComponent()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }


        [HttpGet]
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
