using FoodPoint_Seller.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Models
{
    public class PayedOrder
    {
        public OrderItem Order;

        public string CustomerName;

        public TimeSpan OrderTime;

        public Timer CloseOrderTimer;
        public bool IsOrderFisihed = false;

        public Action<PayedOrder> func;



        public PayedOrder() { }

        public PayedOrder(string customerName, OrderItem order, TimeSpan Time, Action<PayedOrder> func)
        {
            this.CustomerName = customerName;
            this.Order = order;
            this.OrderTime = Time;
            this.func = func;
        }
        public void OnFinishOrder()
        {
            var a = this;
            MvvmCross.Platform.Mvx.GetSingleton<MvvmCross.Plugins.Messenger.IMvxMessenger>().Publish(new ClickOnFinishOrderMessage(this));
            //this.ListOrderItem.Value.RemoveAll(o => o.Order.ID == Order.ID);

            //UpdatePayedOrderList();
            //_sellerOrderService.DeletOrder(Order);
        }

        public PayedOrder(string whoSold, OrderItem deserializeOrder, TimeSpan timer)
        {
            this.CustomerName = whoSold;
            this.Order = deserializeOrder;
            this.OrderTime = timer;
        }

        public PayedOrder Clone(PayedOrder order)
        {
            return new PayedOrder()
            {
                Order = order.Order,
                CustomerName = order.CustomerName,
                OrderTime = order.OrderTime,
                IsOrderFisihed = order.IsOrderFisihed,
                CloseOrderTimer = order.CloseOrderTimer,
                func = order.func,
            };
        }

        internal void StartTimer()
        {
            this.func.Invoke(this);
            this.CloseOrderTimer.StartTimer();
        }

        public void StopTimer()
        {
            this.CloseOrderTimer.StopTimer();
        }
    }
}
