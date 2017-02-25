using FoodPoint_Seller.Core.Models;
using MvvmCross.FieldBinding;
using System.Collections.Generic;
using System;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Api.Models.ViewModels;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class StatisticSellerViewModel : BaseViewModel
    {
        private IStatisticController _statisticController;
        private ISellerAuthService _sellerAuthService;

        //public INC<List<SellerDayInfo>> SellerStatisticListItem = new NC<List<SellerDayInfo>>(new List<SellerDayInfo>() { new SellerDayInfo("1",1,1,1,1), new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) , new SellerDayInfo("1", 1, 1, 1, 1) }, (e) => {
        //});

        public INC<List<SellerDayInfo>> SellerStatisticListItem = new NC<List<SellerDayInfo>>(new List<SellerDayInfo>(), (e) => {
        });

        public INC<string> example = new NC<string>("test", (e) => {
        });

        public StatisticSellerViewModel(IStatisticController statisticController, ISellerAuthService sellerAuthService)
        {
            this._statisticController = statisticController;            

            this._sellerAuthService = sellerAuthService;
        }

        public override void Start()
        {
            base.Start();
            this.Init();
            //this.ListOrderItem.Value = _orderService.GetOrders();
        }

        private async void Init()
        {
            var seller = await _sellerAuthService.GetProfileSeller();
            var token = await _sellerAuthService.GetToken();
            var statisticInfo = await _statisticController.GetSellerStatisticForDay(seller.ID.ToString(), "2016-10-30 00:00:00", 
                                                                                                                    "2016-12-20 00:00:00", token);
            //var a = this.SellerStatisticListItem.Value;
            if (statisticInfo != null)
            {
                this.SellerStatisticListItem.Value = statisticInfo;
            }

        }
    }
}