using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodPoint_Seller.Core.Converters.FoodStatisticValueConverter;

namespace FoodPoint_Seller.Core.Converters
{
    public class FoodStatisticValueConverter : MvxValueConverter<List<FoodDayInfo>, List<FoodDayInfoUI>>
    {
        protected override List<FoodDayInfoUI> Convert(List<FoodDayInfo> foodsInfo, Type targetType, object parameter, CultureInfo culture)
        {
            var newFoodsInfo = foodsInfo.Select(o => new FoodDayInfoUI()
            {
                name = o.name,
                withSubs = o.withSubs.ToString(),
                revenueSubs = o.revenueSubs.ToString(),
                withoutSubs = o.withoutSubs.ToString(),
                revenueNoSubs = o.revenueNoSubs.ToString(),
                commonSellingAmount = o.commonSellingAmount.ToString(),
                //revenueNoSubs = o.revenueNoSubs.ToString()
            }).ToList();

            newFoodsInfo.Insert(0, new FoodDayInfoUI()
            {
                name = "Товар",
                withSubs = "Кол-во продаж подписок",
                revenueSubs = "Выручка с подпиисок",
                withoutSubs = "Кол-во продаж предзаказов",
                revenueNoSubs = "Выручка с предзаказов",
                commonSellingAmount = "Дата",
                //revenueSubs = "Предзаказы",

            });

            return newFoodsInfo;
        }

        public class FoodDayInfoUI
        {
            public string name;
            public string withSubs;
            public string revenueSubs;
            public string withoutSubs;
            public string revenueNoSubs;
            public string commonSellingAmount;

        }
    }

    
}
