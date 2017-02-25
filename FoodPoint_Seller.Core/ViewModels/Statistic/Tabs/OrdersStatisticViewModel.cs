using Acr.UserDialogs;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public  class OrdersStatisticViewModel :  MvxViewModel

    {
        private IStatisticController _statisticController;
        private IOwnerAuthService _ownerAuthService;

        private ISellerAuthService _loginService;

        string formatsrc = "yyyy-MM-dd HH:mm:ss";
    
        //string formatdst = "dd:MM:yyyy";


        public INC<List<RevenueDayInfo>> OrderStatisticListItem = new NC<List<RevenueDayInfo>>(new List<RevenueDayInfo>(), (e) =>
        {
        });


        public INC<DateTime> StartDateValue = new NC<DateTime>(DateTime.Now.AddDays(-7), (e) => {
        });

        public INC<DateTime> EndDateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });
        private IUserDialogs _dialogs;

        public OrdersStatisticViewModel(IStatisticController statisticController, IOwnerAuthService ownerAuthService, ISellerAuthService loginService, IUserDialogs dialogService)
        {
            this._dialogs = dialogService;
            this._statisticController = statisticController;
            this._ownerAuthService = ownerAuthService;
            this._loginService = loginService;
        }

        public override void Start()
        {
            base.Start();
            this.Init();
            //this.ListOrderItem.Value = _orderService.GetOrders();
        }

        private async void Init()
        {
            var user = await this._loginService.GetProfileSeller();
            var token = await _ownerAuthService.GetToken();

            var startDate = StartDateValue.Value.ToString(formatsrc);
            var endDate = EndDateValue.Value.ToString(formatsrc);

            var revenueStatistic = await _statisticController.GetRevenueStatisticForDay(user.shopID.ToString(), startDate, endDate, token);
            OrderStatisticListItem.Value = revenueStatistic;
        }

        public async void SetStartTime()
        {
            var config = new DatePromptConfig();
            var dialogDate = _dialogs.DatePrompt(config);

            config.OnAction = async result => {
                this.StartDateValue.Value = result.SelectedDate;
                await GetStatistic();
            };
        }
        
        public async void SetEndTime()
        {
            var config = new DatePromptConfig();
             var dialogDate = _dialogs.DatePrompt(config);

            config.OnAction = async result => {
                this.EndDateValue.Value = result.SelectedDate;
                await GetStatistic();
            };
        }
        private async Task GetStatistic()
        {
            var user = await this._loginService.GetProfileSeller();
            var token = await _ownerAuthService.GetToken();
            var revenueStatistic = await _statisticController.
                                GetRevenueStatisticForDay(user.shopID.ToString(), StartDateValue.Value.ToString(formatsrc), EndDateValue.Value.ToString(formatsrc), token);
            OrderStatisticListItem.Value = revenueStatistic;
        }

    }
}
