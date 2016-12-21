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

        public Action<PayedOrder> func;

        public PayedOrder(string customerName, OrderItem order, TimeSpan Time, Action<PayedOrder> func)
        {
            this.CustomerName = customerName;
            this.Order = order;
            this.OrderTime = Time;
            this.func = func;
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
