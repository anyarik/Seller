using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodPoint_Seller.Core.Converters.SellerStatisticValueConverter;

namespace FoodPoint_Seller.Core.Converters
{
    public class SellerStatisticValueConverter : MvxValueConverter<List<SellerDayInfo>, List<SellerDayInfoUI>>
    {
        protected override List<SellerDayInfoUI> Convert(List<SellerDayInfo> sellersInfo, Type targetType, object parameter, CultureInfo culture)
        {
            var newSellersInfo = sellersInfo.Select(o => new SellerDayInfoUI()
            {
                date = o.date.ToString("dd-MM-yyyy"),
                refusedOrders = o.refusedOrders.ToString(),
                nonanweredOrders = o.nonanweredOrders.ToString(),
                overedOrders = o.overedOrders.ToString(),
                revenue = o.revenue.ToString()
            }).ToList();

            newSellersInfo.Insert(0, new SellerDayInfoUI()
            {
                date = "Дата",
                refusedOrders = "Кол-во отказов",
                nonanweredOrders = "Кол-во заказов оставленных без ответа",
                overedOrders = "Кол-во выполненных заказов",
                revenue = "Сделанная выручка"
            });

            return newSellersInfo;
        }

        public class SellerDayInfoUI
        {
            public string date;
            public string refusedOrders;
            public string nonanweredOrders;
            public string overedOrders;
            public string revenue;
        }
    }

    
}
