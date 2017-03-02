using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class OnlineSellerDayInfo
    {
        public string Name;
        public string Action;
        public DateTime Moment;


        public OnlineSellerDayInfo()
        {

        }

        //public SellerDayInfo(string date, int refusedOrders, int nonanweredOrders, int overedOrders, float revenue)
        //{
        //    this.date = date;
        //    this.nonanweredOrders = nonanweredOrders;
        //    this.overedOrders = overedOrders;
        //    this.refusedOrders = refusedOrders;
        //    this.revenue = revenue;

        //}
    }
}
