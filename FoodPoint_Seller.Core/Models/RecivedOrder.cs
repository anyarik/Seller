using FoodPoint_Seller.Api.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Models
{
    public  class RecivedOrder
    {
        public OrderItem Order;

        public string CustomerName;
        public string Time;
        //public bool IsActiveOrder = false;
        public bool StatusOrder = true;

        public string DelayTime = null;
        public List<ProductForOrder> DelayProduct = new List<ProductForOrder>();

        public Timer CloseOrderTimer;

        public Action<RecivedOrder> func;
        internal bool IsAlive = false;

        public RecivedOrder(string customerName, string time, OrderItem order, Action<RecivedOrder> func )
        {
            this.CustomerName = customerName;
            this.Time = time;
            this.Order = order;
            this.func = func;
        }
        public RecivedOrder()
        {

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

        ////Информация о полученом заказе, который в обработке продовцом
        //public static OrderItem _recivedOrder;
        //public static bool _recivedStatusOrder = true;
        //public static string _recivedNameCustomer = null;
        //public static string _recivedDelayTime = null;
        //public static List<ProductForOrder> _recivedDelayProduct = new List<ProductForOrder>();
    }
}
