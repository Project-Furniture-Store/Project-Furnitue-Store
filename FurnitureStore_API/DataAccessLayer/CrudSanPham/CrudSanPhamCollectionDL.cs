﻿
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
        private readonly IMongoCollection<UpdateProductPatchResquest> _mongoCollectionUD;


        // Constructor của Data Layer, thêm IConfiguration để đọc cấu hình và khởi tạo kết nối đến MongoDB
        public CrudSanPhamCollectionDL(IConfiguration configuration)
        {
            _configuration = configuration;
            _mongoClient = new MongoClient(_configuration["Database:ConnectionString"]);
            var _mongoDatabase = _mongoClient.GetDatabase(_configuration["Database:DatabaseName"]);
            var collectionName = _configuration["Database:Collections:Collection_SANPHAM"]; // Lấy tên collection từ cấu hình
            _mongoCollection = _mongoDatabase.GetCollection<InsertSanPhamResquest>(collectionName);
            _mongoCollectionUD = _mongoDatabase.GetCollection<UpdateProductPatchResquest>(collectionName);



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
                ObjectId categoryIdObject = ObjectId.Parse(categoryId);
                response.data = await _mongoCollection.Find(x => (x.Loai== categoryIdObject)).ToListAsync();
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

        public async Task<GetSanPhamResponse> GetSanPhambyId(string id)
        {
            GetSanPhamResponse response = new GetSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                response.data = new List<InsertSanPhamResquest>();
                response.data = await _mongoCollection.Find(x => (x.id == id)).ToListAsync();
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

        public async Task<InsertSanPhamResponse> SetProduct(InsertSanPhamResquest Sanpham)
        {
            InsertSanPhamResponse response = new InsertSanPhamResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {
                await _mongoCollection.InsertOneAsync(Sanpham);
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

        public Task<InsertSanPhamResponse> UpdateProductbyID(InsertSanPhamResquest Sanpham)
        {
            throw new NotImplementedException();
        }

        //public async Task<InsertSanPhamResponse> UpdateProductbyID(InsertSanPhamResquest Sanpham)
        //{
        //    InsertSanPhamResponse response = new InsertSanPhamResponse();

        //    // Khởi tạo giá trị mặc định cho phản hồi
        //    response.IsSuccess = true;
        //    response.Message = "Data Successfully";

        //    try
        //    {
        //        GetSanPhamResponse response1 = await GetSanPhambyId(Sanpham.id);

        //        var result= await _mongoCollection.ReplaceOneAsync(x => x.id == Sanpham.id, Sanpham);
        //        if(!result.IsAcknowledged)
        //        {
        //            response.Message = "ErrorL id not found/ not update";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Xử lý lỗi nếu có lỗi xảy ra trong quá trình thực hiện
        //        response.IsSuccess = false;
        //        response.Message = "Error" + ex.Message;
        //    }

        //    // Trả về phản hồi
        //    return response;

        //}

        public async Task<UpdateProductPatchResponse> UpdateProductbyIDPatch(UpdateProductPatchResquest Sanpham1)
        {
            UpdateProductPatchResponse response = new UpdateProductPatchResponse();

            // Khởi tạo giá trị mặc định cho phản hồi
            response.IsSuccess = true;
            response.Message = "Data Successfully";

            try
            {

                var filter = new BsonDocument
                {
                    { "TenSP", Sanpham1.TenSP },
                    { "MieuTa", Sanpham1.MieuTa },
                    { "DonGia", new BsonDouble(Sanpham1.DonGia) },
                    { "MauSac", new BsonArray(Sanpham1.MauSac) },
                    { "KichCo", Sanpham1.KichCo },
                    { "Hinh", Sanpham1.Hinh },
                    { "Supplier", new BsonDocument
                        {
                            { "TenNCC", Sanpham1.NhaCungCap.TenNCC },
                            { "DiaChi", Sanpham1.NhaCungCap.DiaChi },
                            { "SDT", Sanpham1.NhaCungCap.SDT },
                            { "DonGiaCC", new BsonDouble(Sanpham1.NhaCungCap.DonGiaCC) }
                        }
                    },
                    { "GoldCoin", Sanpham1.GoldCoin },
                    { "Loai", Sanpham1.Loai }
                };

                var update = new BsonDocument("$set", filter);
                    var result = await _mongoCollection.UpdateOneAsync(x => x.id == Sanpham1.id, update);
                if (!result.IsAcknowledged)
                {
                    response.Message = "ErrorL id not found/ not update";
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
