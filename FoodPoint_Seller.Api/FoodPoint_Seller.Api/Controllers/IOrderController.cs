using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Controllers
{
    public interface IOrderController
    {
        void HubConnection();
        void CorrectOrder(string customerId, bool status, string order, string time);
        void SendOrder(string order);
        void OnReceiveOrder(Action<object, string, string> func);
        void OnCustomerAgreed(Action<object, bool> func);
        void HubDisconnect();
    }
}
