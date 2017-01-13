using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
   public  interface IOrderHubService 
    {
        void HubConnection(string id);
        void HubDisconnect();
        void CorrectOrder(string customerId, bool status, string corectOrder, string corectTime);
        event EventHandler<IDictionary<string,string>> receiveOrder;
        event EventHandler<bool> customerAgreedYAY;

        event EventHandler<string> setStatusSeller;

    }
}
