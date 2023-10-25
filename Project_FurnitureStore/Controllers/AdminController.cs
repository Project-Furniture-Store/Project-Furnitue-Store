using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_FurnitureStore.Models;
using Project_FurnitureStore.Models.OtherModel;
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


        


        public async Task<IActionResult> AddProductAdmin(string? succces)
        {

           await loadLoaiHang();
          
                ViewBag.SuccessMessage = TempData["succces"];
            

            
            return View();
        }

        public async Task<IActionResult> DeleteProductByID(string id)
        {

            try
            { 

                HttpResponseMessage response1 = await _client.DeleteAsync($"https://localhost:7143/api/SanPham/DeleteProductbyID?id={id}");

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();
                    TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công.";

                    return RedirectToAction("AddProductAdmin",new { succces= "Sản phẩm đã được xóa thành công." });
                }
                else
                {
                    var errorContent = await response1.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa: " + error.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gọi API: " + ex.Message;
            }

            return View();

        }




        public async Task<IActionResult> UpdateProductEX(string id, string tensp, string mieutta, int dongia, string mausac, string kichco, string hinh, string nhacc, string dcncc, string sdt, string loai)
        {
            string mausacc = mausac;
            List<string> mausacList = mausacc.Split('/').ToList();

            UpdateProduct data1 = new UpdateProduct
            {
                id = id,
                TenSP = tensp,
                MieuTa = mieutta,
                DonGia = dongia,
                MauSac = mausacList,
                KichCo = kichco,
                Hinh = hinh,
                GoldCoin = 0,

                NhaCungCap = new Supplierr
                {
                    TenNCC = nhacc,
                    DiaChi = dcncc,
                    SDT = sdt,
                    DonGiaCC = 0.0
                },
                Loai = loai,
            };

            try
            {
                var jsonData = JsonConvert.SerializeObject(data1);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await _client.PatchAsync("https://localhost:7143/api/SanPham/UpdateProductbyIDPatch", content);

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();
                    TempData["SuccessMessage"] = "Sản phẩm đã được cập nhật thành công.";

                    return RedirectToAction("AddProductAdmin");
                }
                else
                {
                    var errorContent = await response1.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật sản phẩm: " + error.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gọi API: " + ex.Message;
            }

            return View();

        }






        public async Task<IActionResult> AddProduct(string tensp, string mieutta, int dongia, string mausac, string kichco, string hinh, string nhacc, string dcncc, string sdt, string loai)
        {
            string mausacc = mausac;
            List<string> mausacList = mausacc.Split('/').ToList();

            SanPhamViewModel data1 = new SanPhamViewModel
            {
                TenSP = tensp,
                MieuTa = mieutta,
                DonGia = dongia,
                MauSac = mausacList,
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

        public async Task<IActionResult> AddFlashSale()
		{
           

            List<FlashSaleViewModel> flashsaleList = new List<FlashSaleViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/FlashSale/GetAllFlashSale");

            if (response.IsSuccessStatusCode)
            {
               
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    flashsaleList = JsonConvert.DeserializeObject<List<FlashSaleViewModel>>(dataJson);
                }

            }
            return View(flashsaleList);
           
		}

        public async Task loadInforFlashSale(string idFs)
        {
            List<FlashSaleViewModel> flashsaleList = new List<FlashSaleViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/FlashSale/GetFlashSale?idfs={idFs}");

            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    flashsaleList = JsonConvert.DeserializeObject<List<FlashSaleViewModel>>(dataJson);
                }

            }
            TempData["FlashSalelist"] = flashsaleList;

        }


        public async Task loadInforNoFlashSale()
        {
            List<SanPhamViewModel> flashsaleList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/SanPham/GetSanPhamNoFlashSale");

            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    flashsaleList = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(dataJson);
                }

            }
            TempData["ProductNoFS"] = flashsaleList;

        }
        public async Task<IActionResult> DetailSanPhamFlashsale(string idflashsale)
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
                    await loadInforFlashSale(idflashsale);
                    await loadInforNoFlashSale();
                }

            }

            return View(sanphamList);
          
        }

        public async Task<IActionResult> ModifyFlashSale(string idFs)
        {
            List<FlashSaleViewModel> flashsale = new List<FlashSaleViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/FlashSale/GetFlashSale?idfs={idFs}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    flashsale = JsonConvert.DeserializeObject<List<FlashSaleViewModel>>(dataJson);

                }

            }
            return View(flashsale);
        }


        public async Task<IActionResult> EXUpdateFlashsale(string idfs,string ngay, string phantram, List<string> idSPs)
        {
            foreach (string idsp in idSPs)
            {
                string iddd = idsp;
            }

            UpdataFlashSale data1 = new UpdataFlashSale
            {
                _id = idfs,
                NgaySale = ngay,
                PhanTramGiam = int.Parse(phantram),
                KhungGio = idSPs
            };

            try
            {
                var jsonData = JsonConvert.SerializeObject(data1);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await _client.PatchAsync("https://localhost:7143/api/FlashSale/UpdateFlashSalebyIDPatch", content);

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();

                    return RedirectToAction("AddProductVoucher", new { idKhuyenMai = idfs });
                }
                else
                {
                    var errorContent = await response1.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật sản phẩm: " + error.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gọi API: " + ex.Message;
            }
            return View();

        }
            
        


        public async Task<IActionResult> EXAddSanPhamFlashsale(string idfs, List<string> idSPs)
        {
            string idSPsJson = JsonConvert.SerializeObject(idSPs);


            StringContent content = new StringContent(idSPsJson, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/FlashSale/AddProductIDFs?fsId={idfs}", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return RedirectToAction("DetailSanPhamFlashsale", new { idflashsale = idfs });
            }
            else
            {
                string errorMessage = response.ReasonPhrase;

            }
            return View();
        }

        public async Task<IActionResult> EXDeleteSanPhamFlashsale(string idfs, List<string> idSPs)
        {
            string idSPsJson = JsonConvert.SerializeObject(idSPs);


            StringContent content = new StringContent(idSPsJson, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/FlashSale/DeleteProductIDFs?fsId={idfs}", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return RedirectToAction("DetailSanPhamFlashsale", new { idflashsale = idfs });
            }
            else
            {
                string errorMessage = response.ReasonPhrase;

            }
            return View();
        }


        public async Task<IActionResult> AddKhuyenMai()
        {

            return View();
        }

        public async Task<IActionResult> EXAddKhuyenMai(string tenkm, string hinhname, string start, string end, string dieukien, string phantram)
        {
            
            KhuyenMaiViewModel data1 = new KhuyenMaiViewModel
            {
                TenKhuyenMai = tenkm,
                Hinh = hinhname,
                NgayKhuyenMai = start,
                NgayKetThuc = end,
                DieuKien = dieukien,
                TienGiam = phantram,
                IdSP = new List<String>() // Khởi tạo danh sách rỗng
            };

            try
            {
                var jsonData = JsonConvert.SerializeObject(data1);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await _client.PostAsync("https://localhost:7143/api/KhuyenMai/SetKhuyenMai", content);

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();
                    return RedirectToAction("AddKhuyenMai");
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



        public async Task<IActionResult> AddProductVoucher(string idKhuyenMai)
        {
            List<KhuyenMaiViewModel> Khuyenmai = new List<KhuyenMaiViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhuyenMai/GetKhuyenMaibyId?idKhuyenMai={idKhuyenMai}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    Khuyenmai = JsonConvert.DeserializeObject<List<KhuyenMaiViewModel>>(dataJson);
                    await loadProducKhuyenMainyID(idKhuyenMai);
                    await loadProducNoKhuyenMainyID();
                }

            }
            return View(Khuyenmai);
        }



        public async Task<IActionResult> ModifyVoucher(string idKhuyenMai)
        {
            List<KhuyenMaiViewModel> Khuyenmai = new List<KhuyenMaiViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhuyenMai/GetKhuyenMaibyId?idKhuyenMai={idKhuyenMai}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    Khuyenmai = JsonConvert.DeserializeObject<List<KhuyenMaiViewModel>>(dataJson);
                  
                }

            }
            return View(Khuyenmai);
        }

        public async Task<IActionResult> EXDeleteModifyVoucher(string idkhuyenmai)
		{
            try
            {

                HttpResponseMessage response1 = await _client.DeleteAsync($"https://localhost:7143/api/KhuyenMai/DeleteKhuyenMaibyID?idkhuyenmai={idkhuyenmai}");

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();
                    TempData["SuccessMessage"] = "Sản phẩm đã được xóa thành công.";

                    return RedirectToAction("AddKhuyenMai");
                }
                else
                {
                    var errorContent = await response1.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi xóa: " + error.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gọi API: " + ex.Message;
            }

            return View();
        }


        public async Task<IActionResult> EXModifyVoucher(string idKhuyenMai, string tenkm, string hinhname, string start, string end, string dieukien, string phantram)
        {
           

            UpdateKhuyenMai data1 = new UpdateKhuyenMai
            {
                _id=idKhuyenMai,
                TenKhuyenMai=tenkm,
                Hinh=hinhname,
                NgayKhuyenMai=start,
                NgayKetThuc=end,
                DieuKien=dieukien,
                TienGiam=phantram
            };

            try
            {
                var jsonData = JsonConvert.SerializeObject(data1);
                StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response1 = await _client.PatchAsync("https://localhost:7143/api/KhuyenMai/UpdateKhuyenMaibyIDPatch", content);

                if (response1.IsSuccessStatusCode)
                {
                    var responseData = await response1.Content.ReadAsStringAsync();

                    return RedirectToAction("AddProductVoucher", new { idKhuyenMai = idKhuyenMai });
                }
                else
                {
                    var errorContent = await response1.Content.ReadAsStringAsync();
                    var error = JsonConvert.DeserializeObject<ErrorResponse>(errorContent);
                    TempData["ErrorMessage"] = "Có lỗi xảy ra khi cập nhật sản phẩm: " + error.Message;
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gọi API: " + ex.Message;
            }

            return View();
        }
        public async Task<IActionResult> EXAddProductVoucher(string idKhuyenMai, List<string> idSPs)
        {
            string idSPsJson = JsonConvert.SerializeObject(idSPs);

           
            StringContent content = new StringContent(idSPsJson, Encoding.UTF8, "application/json");

                                                                                          
            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/KhuyenMai/AddProductID?khuyenMaiId={idKhuyenMai}", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return RedirectToAction("AddProductVoucher", new { idKhuyenMai = idKhuyenMai });
            }
            else
            {
                string errorMessage = response.ReasonPhrase;
                
            }
            return View();
        }


        public async Task<IActionResult> EXDeleteProductVoucher(string idKhuyenMai, List<string> idSPs)
        {
            string idSPsJson = JsonConvert.SerializeObject(idSPs);

            ;
            StringContent content = new StringContent(idSPsJson, Encoding.UTF8, "application/json");


            HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + $"/KhuyenMai/DelelteProductKMID?khuyenMaiId={idKhuyenMai}", content);
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                return RedirectToAction("AddProductVoucher", new { idKhuyenMai = idKhuyenMai });
            }
            else
            {
                string errorMessage = response.ReasonPhrase;

            }
            return View();
        }


        public async Task loadProducNoKhuyenMainyID()
        {
            List<SanPhamViewModel> sanphamList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "/SanPham/GetSanPhamNoKhuyenMai");

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
            TempData["KhuyenMaiNoProductlist"] = sanphamList;

        }

        public async Task loadProducKhuyenMainyID(string idKhuyenMai)
        {
            List<SanPhamViewModel> SanPhamList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/KhuyenMai/ListProductKhuyenMai?idKhuyenMai={idKhuyenMai}");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var jsonObject = JsonConvert.DeserializeObject<JObject>(data); // Đọc dữ liệu JSON vào một đối tượng JObject

                if (jsonObject != null && jsonObject["data"] != null)
                {
                    var dataJson = jsonObject["data"].ToString(); // Lấy phần "data" của JSON
                    SanPhamList = JsonConvert.DeserializeObject<List<SanPhamViewModel>>(dataJson);
                }

            }
            TempData["KhuyenMaiProductlist"] = SanPhamList;

        }



        public IActionResult Index()
        {
            return View();
        }

       


        public async Task<IActionResult> updateProduct(string idSanPham)
        {
            await loadLoaiHang();

            List<SanPhamViewModel> sanphamList = new List<SanPhamViewModel>();
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + $"/SanPham/GetSanPhambyId?id={idSanPham}");

            if (response.IsSuccessStatusCode)
            {
                // Xử lý dữ liệu từ response ở đây (ví dụ: KhuyenMaiList = await response.Content.ReadAsAsync<List<KhuyenMaiViewModel>>();)
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
