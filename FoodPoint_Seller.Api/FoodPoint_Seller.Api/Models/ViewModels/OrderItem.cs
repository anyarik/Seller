
using FoodPoint_Seller.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class OrderItem 
    {
        public Guid ID { get; set; }
        public bool Paid { get; set; }
        public string State { get; set; }
        public decimal Summ { get; set; }
        public string RowNumber { get; set; }
        public string WhoSold { get; set; }
        //public TimeSpan Timer { get; set; }
        public TimeSpan Timer { get; set; }
        public ShopModel Establishment { get; set; }
        public List<ProductForOrder> OrderedFood { get; set; }
         
        public OrderItem()
        {

        }

        
        //public Order(Order order)
        //{
        //    this.Paid = order.Paid;
        //    this.State = order.State;
        //    this.Summ = order.Summ;
        //    this.Establishment = order.Establishment;
        //    this.OrderedFood = GetOrderedFoodList(order.OrderedFood);
        //}

        public OrderItem Clone(OrderItem order)
        {
            return new OrderItem()
            {
                ID = order.ID,
                Paid = order.Paid,
                State = order.State,
                Summ = order.Summ,
                Establishment = order.Establishment,
                OrderedFood = GetOrderedFoodList(order.OrderedFood)
            };
        }

        public OrderItem(ShopModel shop, List<ProductForOrder> orderedFood) 
        {
            this.Paid = false;
            this.State = "start";
            this.Summ = GetSumm(orderedFood);
            this.Establishment = shop;
            this.OrderedFood = orderedFood;
        }

        public OrderItem(Guid id, ShopModel shop, List<ProductForOrder> orderedFood): this(shop, orderedFood)
        {
            this.ID = id;
        }

        private decimal GetSumm(List<ProductForOrder> products)
        {
            decimal summ = 0;

            foreach (var product in products)
            {
                summ += product.ProductInfo.Price;

                foreach (var additive in product.ProductInfo.OrderedAdditives)
                {
                    summ += additive.AdditivePrice;
                }
            }

            return summ;
        }

        private List<ProductForOrder> GetOrderedFoodList(List<ProductForOrder> orderedFood)
        {
            var orderedFoodList = new List<ProductForOrder>();

            foreach (var product in orderedFood)
            {
                orderedFoodList.Add(new ProductForOrder(product));
            }

            return orderedFoodList;
        }
    }
}
