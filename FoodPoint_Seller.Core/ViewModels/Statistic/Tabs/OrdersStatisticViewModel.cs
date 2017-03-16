using Acr.UserDialogs;
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

namespace FoodPoint_Seller.Core.ViewModels
{
    public  class OrdersStatisticViewModel : BaseStatisticViewModel<RevenueDayInfo>
    {
        public OrdersStatisticViewModel(IStatisticController statisticController
                                        , IOwnerAuthService ownerAuthService
                                        , IUserDialogs dialogs)
            : base(statisticController, ownerAuthService, dialogs)
        {
        }

        protected override async  Task GetStatistic()
        {
            var user = await _ownerAuthService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            var revenueStatistic = await _statisticController.
                                GetRevenueStatisticForDay(user.shopID.ToString()
                                                         , StartDateValue.Value.ToString(formatDateWithTime)
                                                         , EndDateValue.Value.ToString(formatDateWithTime)
                                                         , token);
            StatisticListItem.Value = revenueStatistic;
        }
    }
}
