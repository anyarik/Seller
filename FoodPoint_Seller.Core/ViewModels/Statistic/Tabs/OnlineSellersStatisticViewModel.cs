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
using FoodPoint_Seller.Api.Models.DomainModels;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class OnlineSellersStatisticViewModel : BaseStatisticViewModel<OnlineSellerDayInfo>
    {

        public INC<DateTime> DateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });


        public OnlineSellersStatisticViewModel(IStatisticController statisticController
                                 , IOwnerAuthService ownerAuthService
                                 , ISellerAuthService loginService
                                 , IUserDialogs dialogService
                                 , ISellerOrderService sellerOrderService) 
            :base(statisticController, ownerAuthService, loginService, dialogService, sellerOrderService)
        {
        }




        protected override async Task GetStatistic()
        {
            base.GetStatistic();
            var user = await _loginService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            var sellerStatistic = await _statisticController.
                                GetOnlineSellersStatisticForDay(CurrentSeller.Value.ID.ToString(), DateValue.Value.ToString(formatDate), token);
            StatisticListItem.Value = sellerStatistic;
        }

        private void ShopSellers_Changed(object sender, EventArgs e)
        {
            GetStatistic();
        }

        public override async void Start()
        {
            base.Start();

            var user = await _loginService.GetProfile();
            var token = await _ownerAuthService.GetToken();
            ShopSellers.Value = await _statisticController.GetShopSellers(user.shopID.ToString(), token);

            CurrentSeller.Changed += ShopSellers_Changed;
        }

        public void SetDate()
        {
            var config = new DatePromptConfig();
            var dialogDate = _dialogs.DatePrompt(config);

            config.OnAction = async result => {
                if (result.Ok)
                {
                    this.DateValue.Value = result.SelectedDate;
                    await GetStatistic();
                }
            };
        }

    }
}
