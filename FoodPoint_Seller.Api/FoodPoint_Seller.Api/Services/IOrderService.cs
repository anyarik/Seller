using FoodPoint_Seller.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
    public interface IOrderService
    {
        void SendOrder(string order);
        void ChangeStatusOrder(string id, string state, bool isActive, TimeSpan time, string crashState, bool isOverPrevTimer);
        void SetSellerOrder(string orderId, string sellerID);
        Task<List<OrderItem>> GetActiveOrders(string sellerID, string token);
        void SaveOfferedFood(string orderId, List<ProductForOrder> offedProduct, string token); 
    }
}
