using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class ProductInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public CategoryProductModel Category { get; set; }
        public decimal Price { get; set; }
        public List<AdditiveForProduct> OrderedAdditives { get; set; }

    }
}
