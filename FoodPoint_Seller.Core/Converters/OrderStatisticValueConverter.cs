using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodPoint_Seller.Core.Converters.OrderStatisticValueConverter;

namespace FoodPoint_Seller.Core.Converters
{
    public class OrderStatisticValueConverter : MvxValueConverter<List<RevenueDayInfo>, List<RevenueDayInfoUI>>
    {
        protected override List<RevenueDayInfoUI> Convert(List<RevenueDayInfo> ordersInfo, Type targetType, object parameter, CultureInfo culture)
        {
            var newOrdersInfo = ordersInfo.Select(o => new RevenueDayInfoUI()
            {
                date = o.date.ToString("dd-MM-yyyy"),
                common = o.common.ToString(),
                revenueSubs = o.revenueSubs.ToString(),
                revenueNoSubs = o.revenueNoSubs.ToString()
            }).ToList();

            newOrdersInfo.Insert(0, new RevenueDayInfoUI()
            {
                common = "Общее",
                date = "Дата",
                revenueNoSubs ="Предзаказы",
                revenueSubs = "Подписки"
            });

            return newOrdersInfo;
        }

        public class RevenueDayInfoUI
        {
            public string date;
            public string common;
            public string revenueSubs;
            public string revenueNoSubs;
        }
    }

    
}
