using FurnitureStore_API.Model.DonHang;
using FurnitureStore_API.Model.SanPham;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FurnitureStore_API.DataAccessLayer
{
    public class CrudDonHangCollectionDL : ICrudOperationDL_DonHang
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertDonHangResquest> _mongoCollection;



        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudDonHangCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_DONHANG"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertDonHangResquest>(collectionName);
        }

        public async Task<GetSanPhamResponse> GetBestProduct()
        {
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                var pipeline = new[]
                {
             BsonDocument.Parse("{ $unwind: \"$ChiTietDonHang\" }"),
            BsonDocument.Parse("{ $group: { _id: \"$ChiTietDonHang.SanPham\", totalQuantity: { $sum: \"$ChiTietDonHang.SoLuong\" } } }"),
            BsonDocument.Parse("{ $sort: { totalQuantity: -1 } }"),
            BsonDocument.Parse("{ $limit: 10 }"),
            BsonDocument.Parse("{ $lookup: { from: \"SANPHAM\", localField: \"_id\", foreignField: \"_id\", as: \"copies_sold\" } }"),
            BsonDocument.Parse("{ $unwind: \"$copies_sold\" }"), // Unwind để tách các bản ghi trong copies_sold array
            BsonDocument.Parse("{ $replaceRoot: { newRoot: \"$copies_sold\" } }") // Thay đổi root document thành copies_sold
        };

                List<InsertSanPhamResquest> bestProducts = await _mongoCollection.Aggregate<InsertSanPhamResquest>(pipeline).ToListAsync();

                if (bestProducts.Count == 0)
                {
                    response.Message = "No record found";
                }

                response.data = bestProducts;
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

        public async Task<String> GetSLProduct(string idsp)
        {
            string response = "0";
            try
            {
                var pipeline = new[]
{
    BsonDocument.Parse("{ $unwind: \"$ChiTietDonHang\" }"),
    BsonDocument.Parse($"{{ $match: {{ \"ChiTietDonHang.SanPham\": ObjectId(\"{idsp}\") }} }}"),
    BsonDocument.Parse("{ $count: \"total\" }")
};

                var result = await _mongoCollection.Aggregate<BsonDocument>(pipeline).FirstOrDefaultAsync();

                if (result != null)
                {
                    response = result["total"].ToString(); // Lấy giá trị "total" từ kết quả
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
