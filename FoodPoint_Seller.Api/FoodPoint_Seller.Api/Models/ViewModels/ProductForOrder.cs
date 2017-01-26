
using FoodPoint_Seller.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class ProductForOrder
    {
        public Guid ID { get; set; }
        public ProductInfo ProductInfo { get; set; }
        public List<AdditiveModel> PossibleAdditives { get; set; }
        public int index;
       
        public ProductForOrder()
        {

        }
        public ProductForOrder(ProductForOrder p)
        {
            this.ID = p.ID;
            this.ProductInfo = new ProductInfo()
            {
                ID = p.ProductInfo.ID ,
                Name = p.ProductInfo.Name,
                Price = p.ProductInfo.Price,
                OrderedAdditives = SetAdditives(p.ProductInfo.OrderedAdditives)
            };
            this.PossibleAdditives = p.PossibleAdditives;
        }

        private List<AdditiveForProduct> SetAdditives(List<AdditiveForProduct> orderedAdditives)
        {
            var additiveList = new List<AdditiveForProduct>();

            foreach (var additive in orderedAdditives)
            {
                additiveList.Add(new AdditiveForProduct(additive));
            }

            return additiveList;
        }

        public ProductForOrder(ProductModel p)
        {
            this.ID = Guid.NewGuid();
            this.ProductInfo = new ProductInfo()
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                OrderedAdditives = new List<AdditiveForProduct>()
            };
            this.PossibleAdditives = p.Additives;
        }
    }
}
