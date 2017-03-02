﻿using Acr.UserDialogs;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Base;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels.Statistic.Tabs
{
    public class BaseStatisticViewModel<T>: BaseFragment
    {
        protected IStatisticController _statisticController;
        protected IOwnerAuthService _ownerAuthService;

        protected ISellerAuthService _loginService;
        protected IUserDialogs _dialogs;

        protected readonly string formatDateWithTime = "yyyy-MM-dd HH:mm:ss";
        protected readonly string formatDate = "yyyy-MM-dd";

        public INC<List<T>> StatisticListItem = new NC<List<T>>(new List<T>(), (e) =>
        {
        });

        public INC<DateTime> StartDateValue = new NC<DateTime>(DateTime.Now.AddDays(-7), (e) => {
        });

        public INC<DateTime> EndDateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });

        public INC<List<SellerAccountModel>> ShopSellers = new NC<List<SellerAccountModel>>(new List<SellerAccountModel>(), (e) =>
        {
        });

        public INC<SellerAccountModel> CurrentSeller = new NC<SellerAccountModel>(new SellerAccountModel(), (e) =>
        {
        });



        public BaseStatisticViewModel( IStatisticController statisticController
                                     , IOwnerAuthService ownerAuthService 
                                     , ISellerAuthService loginService
                                     , IUserDialogs dialogService
                                     , ISellerOrderService sellerOrderService)
            :this(sellerOrderService)
        {
            this._dialogs = dialogService;
            this._statisticController = statisticController;
            this._ownerAuthService = ownerAuthService;
            this._loginService = loginService;
        }

        public BaseStatisticViewModel(ISellerOrderService sellerOrderService) : base(sellerOrderService)
        {
            this.Init();

        }

        public async override void Start()
        {
            base.Start();
      

            //this.ListOrderItem.Value = _orderService.GetOrders();
        }


        private async void Init()
        {
            GetStatistic();
        }

        public async void SetStartTime()
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

        public async void SetEndTime()
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
        public void UpdateStatistic()
        {
            GetStatistic();
        }

        protected async virtual Task GetStatistic()
        {

        }

    }
}