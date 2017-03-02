using FoodPoint_Seller.Core.Models;
using MvvmCross.FieldBinding;
using System.Collections.Generic;
using System;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.ViewModels.Statistic.Tabs;
using Acr.UserDialogs;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class StatisticCurrentSellerViewModel : BaseStatisticViewModel<SellerDayInfo>
    {

        public StatisticCurrentSellerViewModel(IStatisticController statisticController
                                              , IOwnerAuthService ownerAuthService
                                              , ISellerAuthService loginService
                                              , IUserDialogs dialogService
                                              , ISellerOrderService sellerOrderService) 
            : base(statisticController, ownerAuthService, loginService, dialogService, sellerOrderService)
        {
        }

        protected override async Task GetStatistic()
        {
            base.GetStatistic();
            var user = await _loginService.GetProfile();
            var token = await _loginService.GetToken();
            var statisticInfo = await _statisticController.GetSellerStatisticForDay(user.ID.ToString(), StartDateValue.Value.ToString(formatDateWithTime),
                                                                                                                EndDateValue.Value.ToString(formatDateWithTime), token);

            StatisticListItem.Value = statisticInfo;
        }

    }
}