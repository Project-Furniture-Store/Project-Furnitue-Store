
using FurnitureStore_API.Model.Other;

using MongoDB.Bson;
using MongoDB.Driver;

namespace FurnitureStore_API.DataAccessLayer
{
    public class CrudFlashSaleCollectionDL : ICrudOperationDL_FlashSale
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertListFlashSaleResquest> _mongoCollection;



        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudFlashSaleCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_FlashSale"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertListFlashSaleResquest>(collectionName);
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
    }
}
