using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodPoint_Seller.Core.Converters.OnlineSellerStatisticValueConverter;

namespace FoodPoint_Seller.Core.Converters
{
    public class OnlineSellerStatisticValueConverter : MvxValueConverter<List<OnlineSellerDayInfo>, List<OnlineSellerDayInfoUI>>
    {
        protected override List<OnlineSellerDayInfoUI> Convert(List<OnlineSellerDayInfo> sellersInfo, Type targetType, object parameter, CultureInfo culture)
        {
            var newSellersInfo = sellersInfo.Select(o => new OnlineSellerDayInfoUI()
            {
                Moment = o.Moment.ToString("dd-MM-yyyy HH:mm:ss"),
                Action = o.Action.ToString(),
            }).ToList();

            newSellersInfo.Insert(0, new OnlineSellerDayInfoUI()
            {
                Moment = "Время",
                Action = "Событие",
            });

            return newSellersInfo;
        }

        public class OnlineSellerDayInfoUI
        {
            public string Moment;
            public string Action;

        }
    }

    
}
