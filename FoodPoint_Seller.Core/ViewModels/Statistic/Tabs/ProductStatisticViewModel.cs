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
    public  class ProductStatisticViewModel : BaseStatisticViewModel<FoodDayInfo>
    {
        public ProductStatisticViewModel(IStatisticController statisticController
                 , IOwnerAuthService ownerAuthService
                 , ISellerAuthService loginService
                 , IUserDialogs dialogService
                  , ISellerOrderService sellerOrderService) 
            :base(statisticController, ownerAuthService, loginService, dialogService, sellerOrderService)
        {
        }


        protected override async Task GetStatistic()
        {
            var user = await _loginService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            var foodStatistic = await _statisticController.GetFoodStatisticForDay(user.shopID.ToString(), StartDateValue.Value.ToString(formatDateWithTime), 
                                                                                                                EndDateValue.Value.ToString(formatDateWithTime), token);

            StatisticListItem.Value = foodStatistic;
        }
    }
}
