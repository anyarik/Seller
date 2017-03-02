using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Platform.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static FoodPoint_Seller.Core.Converters.CustomerStatisticValueConverter;

namespace FoodPoint_Seller.Core.Converters
{
    public class CustomerStatisticValueConverter : MvxValueConverter<List<CustomerDayInfo>, List<CustomerDayInfoUI>>
    {
        protected override List<CustomerDayInfoUI> Convert(List<CustomerDayInfo> customersInfo, Type targetType, object parameter, CultureInfo culture)
        {
            var newCustomersInfo = customersInfo.Select(o => new CustomerDayInfoUI()
            {
                date = o.date.ToString("dd-MM-yyyy"),
                newCumstomersNoSubs = o.newCumstomersNoSubs.ToString(),
                newCustomersSubs = o.newCustomersSubs.ToString(),
                oldCumstomersNoSubs = o.oldCumstomersNoSubs.ToString(),
                oldCustomersSubs = o.oldCustomersSubs.ToString()
            }).ToList();

            newCustomersInfo.Insert(0, new CustomerDayInfoUI()
            {
                date = "Дата",
                oldCumstomersNoSubs = "Постоянные(предзаказы)",
                newCumstomersNoSubs = "Новые(предзаказы)",
                oldCustomersSubs = "Постояные(подписки)",
                newCustomersSubs = "Новые(подписки)"
            });

            return newCustomersInfo;
        }

        public class CustomerDayInfoUI
        {
            public string date;
            public string newCumstomersNoSubs;
            public string newCustomersSubs;
            public string oldCumstomersNoSubs;
            public string oldCustomersSubs;
        }
    }

    
}
