using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Project_FurnitureStore.Models
{
    public class LoaiHangViewModel
    {
        [Required]
        public string id { get; set; }

        [Required]
        public string tenLoai { get; set; }

        [Required]
        public string Anh { get; set; }
    }

}
