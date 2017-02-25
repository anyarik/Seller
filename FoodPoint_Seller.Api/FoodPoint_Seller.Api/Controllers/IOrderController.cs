using FoodPoint_Seller.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Controllers
{
    public interface IOrderController
    {
        void HubConnection(string id);
        void CorrectOrder(string customerId, bool status, string order, string time);
        void SendOrder(string order);
        void ChangeStatusOrder(string id, string state, bool isActive, TimeSpan time, string crashState, bool isOverPrevTimer);
        void SetSellerOrder(string orderId, string sellerID);
        Task<List<OrderItem>> GetActiveOrders(string sellerID, string token);
        void SaveOfferedFood(string orderId, List<ProductForOrder> offeredProduct, string token);


        void OnReceiveOrder(Action<object, string, string> func);
        void OnCustomerAgreed(Action<object, bool> func);
        void OnGettingPurchasedOrders(Action<object, string> func);


        void OnChangeStatusSeller(Action<object, string> func);

        void HubDisconnect();
    }
}
