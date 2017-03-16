using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Statistic.Tabs;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Acr.UserDialogs;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class CustomersStatisticViewModel : BaseStatisticViewModel<CustomerDayInfo>
    {
        public CustomersStatisticViewModel(IStatisticController statisticController
                                         , IOwnerAuthService ownerAuthService
                                         , IUserDialogs dialogs) 
            :base(statisticController, ownerAuthService, dialogs)
        {
        }

        protected override async Task GetStatistic()
        {
            var user = await _ownerAuthService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            var customersStatistic = await  _statisticController.
                                GetCustomersStatisticForDay(user.shopID.ToString()
                                                            , StartDateValue.Value.ToString(formatDateWithTime)
                                                            , EndDateValue.Value.ToString(formatDateWithTime)
                                                            , token);
            StatisticListItem.Value = customersStatistic;
        }
    }
}
