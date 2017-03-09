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
        Task<List<PayedOrder>> GetOrders();
        void DeletOrder(OrderItem deletOrder);

        event EventHandler<PayedOrder> OnNewPayedOrder;
        event EventHandler<string> ChangeStatus;
        string CurentStatus { get; set; }

    }
}
