using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Services.Implementations;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services
{
    public class OrderService:IOrderService
    {
        private bool _isInit;
        private List<OrderItem> _orders;

        private IOrderController _orderController;


        public OrderService(IOrderController orderController)
        {
            this._orderController = orderController;

            _orders = new List<OrderItem>();
            this._isInit = false;
            this.Init();
        }

        private async void Init()
        {
            //this._orders = await _orderController.GetActiveOrders();
            this._orderController.HubConnection();
    
            //this._orderController.OnReceiveOrder((obj, receiveOrder) =>
            //{
            //    var order = JsonConvert.DeserializeObject<OrderItem>(receiveOrder);
            //    this._orders.Add(order);


            //});

            //this._orderController.OnCustomerAgreed((_, state) =>
            //{
            //      //if (state)
            //      //      this.textOrder.Text += $"\nПодтвердил";
            //      //  else
            //      //      this.textOrder.Text += $"\nОтменил";
               
            //});

            this._isInit = true;
        }

        public List<OrderItem> GetOrders()
        {
            return _orders;
        }
    }
}
