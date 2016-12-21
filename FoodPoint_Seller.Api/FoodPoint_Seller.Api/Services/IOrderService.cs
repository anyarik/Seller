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
        void ChangeStatusOrder(string id, string state, bool isActive);
        void SetSellerOrder(string orderId, string sellerID);
    }
}
