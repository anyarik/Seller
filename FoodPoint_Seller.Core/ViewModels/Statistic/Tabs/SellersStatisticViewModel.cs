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
    public class SellersStatisticViewModel : BaseStatisticViewModel<SellerDayInfo>
    {
        public SellersStatisticViewModel(IStatisticController statisticController
         , IOwnerAuthService ownerAuthService
         , ISellerAuthService loginService
         , IUserDialogs dialogService
         , ISellerOrderService sellerOrderService) 
            :base(statisticController, ownerAuthService, loginService, dialogService, sellerOrderService)
        {
        }


        public override async void Start()
        {
            base.Start();

            var user = await _loginService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            ShopSellers.Value = await _statisticController.GetShopSellers(user.shopID.ToString(), token);

            CurrentSeller.Changed += ShopSellers_Changed;
        }
        private void ShopSellers_Changed(object sender, EventArgs e)
        {
            GetStatistic();
        }


        protected override async Task GetStatistic()
        {
            base.GetStatistic();
            var user = await _loginService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            var sellerStatistic = await _statisticController.GetSellerStatisticForDay(CurrentSeller.Value.ID.ToString(), StartDateValue.Value.ToString(formatDateWithTime),
                                                                                                                EndDateValue.Value.ToString(formatDateWithTime), token);

            StatisticListItem.Value = sellerStatistic;
        }
    }
}
