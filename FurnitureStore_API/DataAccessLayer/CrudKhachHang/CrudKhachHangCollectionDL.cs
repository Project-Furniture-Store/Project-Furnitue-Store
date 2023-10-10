using FurnitureStore_API.Model.KhachHang;
using FurnitureStore_API.Model.Other.GioHang;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FurnitureStore_API.DataAccessLayer
{
    public class CrudKhachHangCollectionDL : ICrudOperationDL_KhachHang
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertKhachHangResquest> _mongoCollection;



        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudKhachHangCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_KhachHang"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertKhachHangResquest>(collectionName);
        }

        public async Task<GetKhachHangResponse> AddProductCart(string idkh, string idsp, string mausac, string dongia, string sl, string size)
        {
            GetKhachHangResponse response = new GetKhachHangResponse();
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                var gioHangItem = new GioHangItem
                {
                    SanPhamCart = idsp,
                    DonGia = int.Parse(dongia),
                    MauSac = mausac,
                    SoLuong =int.Parse(sl),
                    KichCo = size
                };

                var filter = Builders<InsertKhachHangResquest>.Filter.Eq("_id", ObjectId.Parse(idkh));
                var update = Builders<InsertKhachHangResquest>.Update.Push("GioHang", gioHangItem);

                var result = await _mongoCollection.UpdateOneAsync(filter, update);

                if (result.IsAcknowledged && result.ModifiedCount > 0)
                {
                    response.IsSuccess = true;
                    response.Message = "Data Successfully";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No data updated";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error" + ex.Message;
            }

            return response;
        }



		public async Task<GetKhachHangResponse> GetKhachHangByID(string account, string pass)
        {
            GetKhachHangResponse response = new GetKhachHangResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Thực hiện thêm dữ liệu vào MongoDB
                // Thực hiện thêm dữ liệu vào MongoDB
                response.data = new List<InsertKhachHangResquest>();
                response.data = await _mongoCollection.Find(x => (x.TaiKhoan == account && x.MatKhau == pass)).ToListAsync();
                if (response.data.Count == 0)
                {
                    response.Message = "No record found";
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

        public async Task<string> GetSLSanPham(string idKH)
        {
            string response = "0";
            try
            {
                var pipeline = new[]
                {
                    BsonDocument.Parse($"{{ $match: {{ \"_id\": ObjectId(\"{idKH}\") }} }}"),
                    BsonDocument.Parse("{ $unwind: \"$GioHang\" }"),
                    BsonDocument.Parse("{ $count: \"totalItems\" }")
                };

                var result = await _mongoCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

                if (result != null)
                {
                    response = result["totalItems"].ToString(); // Lấy giá trị "total" từ kết quả
                }
                else
                {
                    response = "0";
                }
            }
            catch (Exception ex)
            {
                response = "0";
            }

            // Trả về phản hồi
            return response;
        }
    }
}
