using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using System.Text;
using System.Threading.Tasks;

namespace Project_FurnitureStore.Controllers
{
    public class AdminController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7143/api");
        private readonly HttpClient _client;
        public AdminController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAddress;

        }





        ////public async Task<IActionResult> AddProduct(string tensp, string mieutta, int dongia, string mausac, string kichco, string hinh, string nhacc, string dcncc, string sdt, string loai)
        ////        {
        ////            HttpResponseMessage response1 = await _client.GetAsync(_client.BaseAddress + $"/SanPham/SetProduct?TenSP={tensp}&MieuTa={mieutta}&DonGia={dongia}&MauSac={mausac}&KichCo={kichco}&Hinh={hinh}&NhaCungCap.TenNCC={nhacc}&NhaCungCap.DiaChi={dcncc}&NhaCungCap.SDT={sdt}&NhaCungCap.DonGiaCC=0.0&GoldCoin=0&Loai={loai}");


        ////            if (response1.IsSuccessStatusCode)
        ////            {
        ////                string data = await response1.Content.ReadAsStringAsync();
        ////                var jsonObject = JsonConvert.DeserializeObject<JObject>(data);

        ////                if (jsonObject != null && jsonObject["isSuccess"] != null)
        ////                {
        ////                    bool isSuccess = jsonObject.Value<bool>("isSuccess");

        ////                    if (isSuccess)
        ////                    {
        ////                        return RedirectToAction("AddProductAdmin");
        ////                    }
        ////                    else
        ////                    {
        ////                        string message = jsonObject.Value<string>("message");
        ////                        // Xử lý khi có thông báo lỗi từ API (nếu có)
        ////                    }
        ////                }
        ////                else
        ////                {
        ////                    // Xử lý khi dữ liệu JSON không đúng định dạng
        ////                }
        ////            }
        ////            else
        ////            {

        ////            }
        ////            return View();
        ////        }



        public async Task<IActionResult> AddProduct(string tensp, string mieutta, int dongia, List<string> mausac, string kichco, string hinh, string nhacc, string dcncc, string sdt, string loai)
        {
            SanPhamViewModel data1 = new SanPhamViewModel
            {
                TenSP = tensp,
                MieuTa = mieutta,
                DonGia = dongia,
                MauSac = mausac,
                KichCo = kichco,
                Hinh = hinh,

                NhaCungCap = new Supplier
                {
                    TenNCC = nhacc,
                    DiaChi = dcncc,
                    SDT = sdt,
                    DonGiaCC = 0.0
                },
                Loai = loai,
                DanhGia = new List<Rating>() // Khởi tạo danh sách rỗng
            };

            try
            {
                var jsonData = JsonConvert.SerializeObject(data1);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await _client.PostAsync("https://localhost:7143/api/SanPham/SetProduct", content);

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();
                    return RedirectToAction("AddProductAdmin");
                }
                else
                {
                    var errorContent = await response1.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    // Xử lý lỗi ở đây
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình gọi API
                // ex.Message chứa thông tin lỗi
            }

            return View();
        }







        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> AddProductAdmin()
        {

            await loadLoaiHang();
            return View();
        }


        public async Task<List<LoaiHangViewModel>> updateProduct(string idSanPham)
        {
            List<LoaiHangViewModel> LoaiList = new List<LoaiHangViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/LoaiHang/GetLoaiHangByName?name={name}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    LoaiList = JsonConvert.DeserializeObject<List<LoaiHangViewModel>>(dataJson);
                }

            }
            return LoaiList;

        }


        public async Task<List<LoaiHangViewModel>> getIDLoaibyName(string name)
        {
            List<LoaiHangViewModel> LoaiList = new List<LoaiHangViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/LoaiHang/GetLoaiHangByName?name={name}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    LoaiList = JsonConvert.DeserializeObject<List<LoaiHangViewModel>>(dataJson);
                }

            }
            return LoaiList;

        }



        public async Task loadKhuyenMai()
        {
            List<KhuyenMaiViewModel> KhuyenMaiList = new List<KhuyenMaiViewModel>();
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

    }
}
