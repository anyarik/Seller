
using FoodPoint_Seller.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class AdditiveForProduct
    {
        public Guid ID { get; set; }
        public int IDAdditive { get; set; }
        public string AdditiveName { get; set; }
        public decimal AdditivePrice { get; set; }


        public AdditiveForProduct()
        {

        }

        public AdditiveForProduct(AdditiveForProduct additive)
        {
            this.ID = additive.ID;
            this.IDAdditive = additive.IDAdditive;
            this.AdditiveName = additive.AdditiveName;
            this.AdditivePrice = additive.AdditivePrice;
        }

        public AdditiveForProduct(AdditiveModel a)
        {
            this.ID = Guid.NewGuid();
            this.IDAdditive = a.ID;
            this.AdditiveName = a.Name;
            this.AdditivePrice = a.Price;
        }
    }
}
