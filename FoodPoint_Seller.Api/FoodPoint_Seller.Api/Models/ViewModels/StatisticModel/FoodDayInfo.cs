using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class FoodDayInfo
    {
        public int ID;
        public string name;
        public int withSubs;
        public float revenueSubs;
        public int withoutSubs;
        public float revenueNoSubs;
        public int commonSellingAmount;


        public FoodDayInfo()
        {
        
         
        }
    }
}
