using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class CustomerDayInfo
    {
        public DateTime date;
        public int newCumstomersNoSubs;
        public int newCustomersSubs;
        public int oldCumstomersNoSubs;
        public int oldCustomersSubs;

        public CustomerDayInfo()
        {

        }

        //public CustomerDayInfo(string date, int refusedOrders, int nonanweredOrders, int overedOrders, float revenue)
        //{
        //    this.date = date;
        //    this.nonanweredOrders = nonanweredOrders;
        //    this.overedOrders = overedOrders;
        //    this.refusedOrders = refusedOrders;
        //    this.revenue = revenue;

        //}
    }
}
