using FoodPoint_Seller.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Controllers.Implementations
{
    public class OrderController : IOrderController
    {
        private IOrderHubService orderHubService;
        private IOrderService orderService;

        public OrderController(IOrderHubService orderHubService, IOrderService orderService)
        {
            this.orderHubService = orderHubService;
            this.orderService =orderService;
    }
        public void HubConnection(string id) => orderHubService.HubConnection(id);
        public void CorrectOrder( string customerId, bool status, string order, string time) => orderHubService.CorrectOrder(customerId, status,order,time);

        public void OnCustomerAgreed(Action<object, bool> func)
        {
            this.orderHubService.customerAgreedYAY += (_, customerAgreedYAY) => { func.Invoke(null, customerAgreedYAY); };
        }
        public void OnReceiveOrder(Action<object, string, string> func)
        {
            this.orderHubService.receiveOrder += (customer, orderDictionary) => { func.Invoke(customer, orderDictionary["order"], orderDictionary["time"]); };
        }
        public void HubDisconnect() => orderHubService.HubDisconnect();

        public void SendOrder(string order) => orderService.SendOrder(order);
        public void ChangeStatusOrder(string id, string state, bool isActive) => orderService.ChangeStatusOrder(id, state, isActive);

        public void SetOrderSeller(string id, string state, bool isActive) => orderService.ChangeStatusOrder(id, state, isActive);

        public void SetSellerOrder(string orderId, string sellerID) =>  orderService.SetSellerOrder(orderId, sellerID);

        public void OnChangeStatusSeller(Action<object, string> func)
        {
            this.orderHubService.setStatusSeller += (_, statusSeller) => { func.Invoke(null, statusSeller); };
        }

        public void OnGettingPurchasedOrders(Action<object, string> func)
        {
            this.orderHubService.gettingPurchasedOrders += (_, order) => { func.Invoke(null, order); };
        }
    }
}
