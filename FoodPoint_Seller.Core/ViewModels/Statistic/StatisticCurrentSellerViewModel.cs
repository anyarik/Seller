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
using FoodPoint_Seller.Core.ViewModels.Base;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class StatisticCurrentSellerViewModel : BaseFragment
    {
        protected IStatisticController _statisticController;
        protected IOwnerAuthService _ownerAuthService;
        
        protected IUserDialogs _dialogs;

        protected readonly string formatDateWithTime = "yyyy-MM-dd HH:mm:ss";
        protected readonly string formatDate = "yyyy-MM-dd";

        public INC<List<SellerDayInfo>> StatisticListItem = new NC<List<SellerDayInfo>>(new List<SellerDayInfo>(), (e) =>
        {
        });

        public INC<DateTime> StartDateValue = new NC<DateTime>(DateTime.Now.AddDays(-7), (e) => {
        });

        public INC<DateTime> EndDateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });

        protected ISellerAuthService _authService;

        public StatisticCurrentSellerViewModel(IStatisticController statisticController
                                              , IUserDialogs dialogs
                                              , ISellerAuthService authService
                                              , ISellerOrderService sellerOrderService) 
            :base(sellerOrderService, authService)
        {
            this._statisticController = statisticController;
            this._dialogs = dialogs;

            this._authService = authService;
        }

        public StatisticCurrentSellerViewModel(ISellerOrderService sellerOrderService, ISellerAuthService authService) 
            : base(sellerOrderService, authService)
        {
        }

        public override void Start()
        {
            base.Start();
            this.Init();
        }

        private async void Init()
        {
            await GetStatistic();
        }

        public void SetStartTime()
        {
            var config = new DatePromptConfig();
            var dialogDate = _dialogs.DatePrompt(config);

            config.OnAction = async result => {
                if (result.Ok)
                {
                    this.StartDateValue.Value = result.SelectedDate;
                    await GetStatistic();
                }
            };
        }

        public void SetEndTime()
        {
            var config = new DatePromptConfig();
            var dialogDate = _dialogs.DatePrompt(config);

            config.OnAction = async result => {
                if (result.Ok)
                {
                    this.EndDateValue.Value = result.SelectedDate;
                    await GetStatistic();
                }
            };
        }
        public async void UpdateStatistic()
        {
           await GetStatistic();
        }

        private  async Task GetStatistic()
        {
            var user = await _authService.GetProfile();
            var token = await _authService.GetToken();
            var statisticInfo = await _statisticController
                            .GetSellerStatisticForDay(user.ID.ToString()
                                                     , StartDateValue.Value.ToString(formatDateWithTime)
                                                     , EndDateValue.Value.ToString(formatDateWithTime)
                                                     , token);

            StatisticListItem.Value = statisticInfo;
        }

    }
}