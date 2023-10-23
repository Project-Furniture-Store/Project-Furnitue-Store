using FurnitureStore_API.Model.KhuyenMai;
using FurnitureStore_API.Model.SanPham;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FurnitureStore_API.DataAccessLayer
{
    public class CrudKhuyenMaiCollectionDL: ICrudOperationDL_KhuyenMai
    {
        private readonly IConfiguration _configuration;
        private readonly MongoClient _mongoClient;
        private readonly IMongoCollection<InsertKhuyenMaiResquest> _mongoCollection;



        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudKhuyenMaiCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_KhuyenMai"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertKhuyenMaiResquest>(collectionName);
        }

        public async Task<GetKhuyenMaiResponse> AddProductID(string khuyenMaiId, List<string> idSPs)
        {
            GetKhuyenMaiResponse response = new GetKhuyenMaiResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";
            try
            {
                List<ObjectId> objectIdList = new List<ObjectId>();

                foreach (string id in idSPs)
                {
                    // Kiểm tra xem chuỗi có chứa khoảng trắng không
                    if (!string.IsNullOrWhiteSpace(id))
                    {
                        // Loại bỏ khoảng trắng ở đầu và cuối chuỗi
                        string trimmedId = id.Trim();

                        // Kiểm tra chuỗi sau khi loại bỏ khoảng trắng có còn trống không
                        if (!string.IsNullOrWhiteSpace(trimmedId))
                        {
                            objectIdList.Add(ObjectId.Parse(trimmedId));
                        }
                    }
                }

                // Thực hiện thêm dữ liệu vào MongoDB
                var filter = Builders<InsertKhuyenMaiResquest>.Filter.Eq("_id", khuyenMaiId);
                var update = Builders<InsertKhuyenMaiResquest>.Update.PushEach("IdSP", objectIdList);

                var result = await _mongoCollection.UpdateOneAsync(filter, update);

                if (result.ModifiedCount == 0)
                {
                    response.IsSuccess = false;
                    response.Message = "Khuyến mãi không tồn tại";
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






        public async Task<GetKhuyenMaiResponse> GetKhuyenMai()
        {
            GetKhuyenMaiResponse response = new GetKhuyenMaiResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Thực hiện thêm dữ liệu vào MongoDB
                // Thực hiện thêm dữ liệu vào MongoDB
                response.data = new List<InsertKhuyenMaiResquest>();
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

        public async Task<GetKhuyenMaiResponse> GetKhuyenMaibyId(string idKhuyenMai)
        {
            GetKhuyenMaiResponse response = new GetKhuyenMaiResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                // Thực hiện thêm dữ liệu vào MongoDB
                // Thực hiện thêm dữ liệu vào MongoDB
                response.data = new List<InsertKhuyenMaiResquest>();
                response.data = await _mongoCollection.Find(x => x._id == idKhuyenMai).ToListAsync();
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

		public async Task<GetSanPhamResponse> ListProductKhuyenMai(string idKhuyenMai)
		{
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {


                var pipeline = new[]
                   {
                        BsonDocument.Parse($"{{ $match: {{ _id: ObjectId('{idKhuyenMai}') }} }}"),
                        BsonDocument.Parse("{ $lookup: { from: 'SANPHAM', localField: 'IdSP', foreignField: '_id', as: 'sanpham' } }"),
                        BsonDocument.Parse("{ $unwind: '$sanpham' }"),
                        BsonDocument.Parse("{ $replaceWith: '$sanpham' }") // Chỉ lấy dữ liệu bên trong 'sanpham'
                    };


                List<InsertSanPhamResquest> listsp = await _mongoCollection.Aggregate<InsertSanPhamResquest>(pipeline).ToListAsync();

                if (listsp.Count == 0)
                {
                    response.Message = "No record found";
                }

                response.data = listsp;


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
