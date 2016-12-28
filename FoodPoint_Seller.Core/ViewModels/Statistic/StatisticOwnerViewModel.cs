using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Services;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public  class StatisticOwnerViewModel: BaseViewModel
    {
        private IStatisticController _statisticController;
        private IOwnerAuthService _ownerAuthService;

        private ISellerAuthService _loginService;

        string formatsrc = "yyyy-MM-dd HH:mm:ss";
    
        //string formatdst = "dd:MM:yyyy";



        public INC<List<SellerDayInfo>> SellerStatisticListItem = new NC<List<SellerDayInfo>>(new List<SellerDayInfo>(), (e) =>
        {
        });

        public INC<List<FoodDayInfo>> FoodStatisticListItem = new NC<List<FoodDayInfo>>(new List<FoodDayInfo>(), (e) =>
        {
        });

        public INC<List<RevenueDayInfo>> OrderStatisticListItem = new NC<List<RevenueDayInfo>>(new List<RevenueDayInfo>(), (e) =>
        {
        });

        public INC<bool> IsFoodStatisticOpen = new NC<bool>(true, (e) => {
        });

        public INC<bool> IsSellerStatisticOpen = new NC<bool>(false, (e) => {
        });

        public INC<bool> IsOrderStatisticOpen = new NC<bool>(false, (e) => {
        });

        public INC<DateTime> StartDateValue = new NC<DateTime>(DateTime.Now.AddDays(-7), (e) => {
        });

        public INC<DateTime> EndDateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });


        public StatisticOwnerViewModel(IStatisticController statisticController, IOwnerAuthService ownerAuthService, ISellerAuthService loginService)
        {
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
            var startDate = StartDateValue.Value.ToString(formatsrc);
            var endDate = EndDateValue.Value.ToString(formatsrc);

            var foodStatistic = await _statisticController.GetFoodStatisticForDay(user.shopID.ToString(), startDate, endDate);
            FoodStatisticListItem.Value = foodStatistic;
        }

        public async void OnProductStatistic()
        {
            if (!IsFoodStatisticOpen.Value)
            {
                IsSellerStatisticOpen.Value = false;
                IsOrderStatisticOpen.Value = false;
                    
                IsFoodStatisticOpen.Value = true;

                var user = await this._loginService.GetProfileSeller();
                var startDate = StartDateValue.Value.ToString(formatsrc);
                var endDate = EndDateValue.Value.ToString(formatsrc);

                var foodStatistic = await _statisticController.GetFoodStatisticForDay(user.shopID.ToString(), startDate, endDate);
                FoodStatisticListItem.Value = foodStatistic;
            }
        }
        public async void OnSellerStatistic()
        {
            if (!IsSellerStatisticOpen.Value)
            {
                IsFoodStatisticOpen.Value = false;
                IsOrderStatisticOpen.Value = false;

                IsSellerStatisticOpen.Value = true;

                var user = await this._loginService.GetProfileSeller();
                var startDate = StartDateValue.Value.ToString(formatsrc);
                var endDate = EndDateValue.Value.ToString(formatsrc);

                var sellerStatistic =  await _statisticController.GetSellerStatisticForDay(user.ID.ToString(), startDate, endDate);
                SellerStatisticListItem.Value = sellerStatistic;
            }
        }
        public async void OnOrderStatistic()
        {
            if (!IsOrderStatisticOpen.Value)
            {
                IsSellerStatisticOpen.Value = false;
                IsFoodStatisticOpen.Value = false;

                IsOrderStatisticOpen.Value = true;
                var user = await this._loginService.GetProfileSeller();
                var startDate = StartDateValue.Value.ToString(formatsrc);
                var endDate = EndDateValue.Value.ToString(formatsrc);

                var revenueStatistic = await _statisticController.GetRevenueStatisticForDay(user.shopID.ToString(), startDate, endDate);
                OrderStatisticListItem.Value = revenueStatistic;
            }
        }
    }
}
