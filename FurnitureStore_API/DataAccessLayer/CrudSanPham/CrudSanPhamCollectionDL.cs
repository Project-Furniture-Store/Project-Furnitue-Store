
using FurnitureStore_API.Model.DonHang;
using FurnitureStore_API.Model.SanPham;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace FurnitureStore_API.DataAccessLayer
{
    public class CrudSanPhamCollectionDL : ICrudOperationDL_SanPham
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertSanPhamResquest> _mongoCollection;



        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudSanPhamCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_SANPHAM"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertSanPhamResquest>(collectionName);


          
        }



        

        public async Task<GetSanPhamResponse> GetSanPham()
        {
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Thực hiện thêm dữ liệu vào MongoDB
                // Thực hiện thêm dữ liệu vào MongoDB
                response.data = new List<InsertSanPhamResquest>();
                response.data = await _mongoCollection.Find(x => true).ToListAsync();
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

		public async Task<GetSanPhamResponse> GetSanPhambycateId(string categoryId)
		{
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                response.data = new List<InsertSanPhamResquest>();
                response.data = await _mongoCollection.Find(x => (x.Loai== categoryId)).ToListAsync();
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

        public async Task<GetSanPhamResponse> GetSanPhambyKeyword(string keyword)
        {
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                response.data = new List<InsertSanPhamResquest>();
                var filter = Builders<InsertSanPhamResquest>.Filter.Regex("TenSP", new BsonRegularExpression("/" + keyword + "/i"));
                response.data = await _mongoCollection.Find(filter).ToListAsync();
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
    }
}
