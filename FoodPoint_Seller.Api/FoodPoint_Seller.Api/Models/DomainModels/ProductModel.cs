using System.Collections.Generic;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class ProductModel
    {
        public int ID { get; set; } 
        public string Name { get; set; }
        public List<CategoryModel> Categories; //public byte[] Picture { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<AdditiveModel> Additives { get; set; }
    }
}