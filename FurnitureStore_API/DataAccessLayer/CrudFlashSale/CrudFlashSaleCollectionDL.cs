
using FurnitureStore_API.Model.FlashSale;
using FurnitureStore_API.Model.Other;
using FurnitureStore_API.Model.SanPham;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Bson;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FurnitureStore_API.DataAccessLayer
{
    public class CrudFlashSaleCollectionDL : ICrudOperationDL_FlashSale
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertListFlashSaleResquest> _mongoCollection;
        private readonly IMongoCollection<InsertFlashSaleResquest> _mongoCollectioninti;



        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudFlashSaleCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_FlashSale"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertListFlashSaleResquest>(collectionName);
            _mongoCollectioninti = _mongoDatabase.GetCollection<InsertFlashSaleResquest>(collectionName);
        }

        public async Task<GetFlashSaleResponse> AddProductIDFs(string fsId, List<string> idSPs)
        {
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Kiểm tra xem danh sách idSPs có giá trị không rỗng
                if (idSPs != null && idSPs.Any())
                {
                    List<string> idStrings = JsonConvert.DeserializeObject<List<string>>(idSPs[0]);

                    // Thực hiện thêm dữ liệu vào MongoDB
                    var filter = Builders<InsertFlashSaleResquest>.Filter.Eq("_id", fsId);
                    var update = Builders<InsertFlashSaleResquest>.Update.PushEach("SanPhamSale", idStrings);

                    var result = await _mongoCollectioninti.UpdateOneAsync(filter, update);

                    if (result.ModifiedCount == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Khuyến mãi không tồn tại";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Danh sách IdSPs không chứa các giá trị hợp lệ";
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Lỗi: " + ex.Message;
            }

            // Trả về phản hồi
            return response;
        }

        public async Task<GetFlashSaleResponse> DeleteProductIDFs(string fsId, List<string> idSPs)
        {
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Kiểm tra xem danh sách idSPs có giá trị không rỗng
                if (idSPs != null && idSPs.Any())
                {
                    List<string> idStrings = JsonConvert.DeserializeObject<List<string>>(idSPs[0]);
                    //foreach (var idString in idStrings)
                    //{
                    //    string aa = idString;
                    //}

                    // Thực hiện thêm dữ liệu vào MongoDB
                    var filter = Builders<InsertFlashSaleResquest>.Filter.Eq("_id", fsId);
                    var update = Builders<InsertFlashSaleResquest>.Update.PullAll("SanPhamSale", idStrings);

                    var result = await _mongoCollectioninti.UpdateOneAsync(filter, update);


                    if (result.ModifiedCount == 0)
                    {
                        response.IsSuccess = false;
                        response.Message = "Khuyến mãi không tồn tại";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Danh sách IdSPs không chứa các giá trị hợp lệ";
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Lỗi: " + ex.Message;
            }

            // Trả về phản hồi
            return response;
        }

        public async Task<GetFlashSaleResponse> GetAllFlashSale()
        {
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Thực hiện thêm dữ liệu vào MongoDB
                // Thực hiện thêm dữ liệu vào MongoDB
                response.data = new List<InsertFlashSaleResquest>();
                response.data = await _mongoCollectioninti.Find(x => true).ToListAsync();
                if (response.data.Count == 0)
                {
                    //  response.Message = "No record found";
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi
            return response;
        }

        public async Task<GetFlashSaleResponse> GetFlashSale(string idfs)
        {
            GetFlashSaleResponse response = new GetFlashSaleResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Thực hiện thêm dữ liệu vào MongoDB
                // Thực hiện thêm dữ liệu vào MongoDB
                response.data = new List<InsertFlashSaleResquest>();
                response.data = await _mongoCollectioninti.Find(x => x._id== idfs).ToListAsync();
                if (response.data.Count == 0)
                {
                    //  response.Message = "No record found";
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi
            return response;
        }

        public async Task<GetListFlashSaleResponse> GetFlashSaleFlashSale()
        {
            GetListFlashSaleResponse response = new GetListFlashSaleResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {

                // Thực hiện thêm dữ liệu vào MongoDB
                var currentDate = DateTime.Now.ToString("dd/MM/yyyy");

                var pipeline = new[]
                {
                    BsonDocument.Parse($"{{ $match: {{ Ngay: '{currentDate}' }} }}"),
                    BsonDocument.Parse("{ $unwind: '$SanPhamSale' }"),
                    BsonDocument.Parse("{ $lookup: { from: 'SANPHAM', localField: 'SanPhamSale', foreignField: '_id', as: 'SanPhamInfo' } }"),
                    BsonDocument.Parse("{ $unwind: '$SanPhamInfo' }"),
                    BsonDocument.Parse("{ $addFields: { PhanTramGiam: '$PhanTramGiam' } }"),
                    BsonDocument.Parse("{ $project: { GioSale: '$KhungGio', SanPhamInfo: '$SanPhamInfo', PhanTramGiam: 1 } }")
                };

                List<InsertListFlashSaleResquest> listsale = await _mongoCollection.Aggregate<InsertListFlashSaleResquest>(pipeline).ToListAsync();

                if (listsale.Count == 0)
                {
                    response.Message = "No record found";
                }

                response.data = listsale;


            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi
            return response;
        }

        public async Task<GetSanPhamResponse> GetProductFlashSale(string idFlashSale)
        {
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                var pipeline = new[]
                {
                   BsonDocument.Parse($"{{ $match: {{ _id: ObjectId('{idFlashSale}') }} }}"),
                    BsonDocument.Parse(@"{ $lookup: { from: 'SANPHAM', localField: 'SanPhamSale', foreignField: '_id', as: 'SanPhamInfo' } }"),
                    BsonDocument.Parse(@"{ $unwind: '$SanPhamInfo' }"),
                    BsonDocument.Parse(@"{ $replaceWith: '$SanPhamInfo' }"),
                    BsonDocument.Parse(@"{ $project: { _id: 1, TenSP: 1, MieuTa: 1, DonGia: 1, MauSac: 1, KichCo: 1, Hinh: 1, 'NhaCungCap.TenNCC': 1, 'NhaCungCap.DiaChi': 1, 'NhaCungCap.SDT': 1, 'NhaCungCap.DonGiaCC': 1, 'DanhGia.KhachHangFeeback': 1, 'DanhGia.Rate': 1, 'DanhGia.ChiTiet': 1, 'DanhGia.Hinh': 1, 'DanhGia.Video': 1, GoldCoin: 1, Loai: 1 } }")
                };

                List<InsertSanPhamResquest> listsale = await _mongoCollectioninti.Aggregate<InsertSanPhamResquest>(pipeline).ToListAsync();

                if (listsale.Count == 0)
                {
                    response.Message = "No record found";
                }

                response.data = listsale;


            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            // Trả về phản hồi
            return response;
        }
    }
}
