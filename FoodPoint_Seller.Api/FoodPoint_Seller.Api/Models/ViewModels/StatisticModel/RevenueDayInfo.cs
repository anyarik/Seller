using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.ViewModels
{
    public class RevenueDayInfo
    {
        public DateTime date;

        public float common;
        public float revenueSubs;
        public float revenueNoSubs;

        public RevenueDayInfo()
        {

        }

        public RevenueDayInfo(DateTime date, int common, int revenueSubs, int revenueNoSubs)
        {
            this.date = date;
            this.common = common;
            this.revenueSubs = revenueSubs;
            this.revenueNoSubs = revenueNoSubs;
        }
    }
}
