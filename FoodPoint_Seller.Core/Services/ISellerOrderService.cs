using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services
{
    public interface ISellerOrderService
    {
        List<PayedOrder> GetOrders();
        void DeletOrder(PayedOrder deletOrder);

        event EventHandler<PayedOrder> OnNewPayedOrder;
    }
}
