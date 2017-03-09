using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Base;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public  class StatisticOwnerViewModel: BaseFragment
    {
        private IStatisticController _statisticController;
        private IOwnerAuthService _ownerAuthService;

        private ISellerAuthService _loginService;
    
        public INC<DateTime> StartDateValue = new NC<DateTime>(DateTime.Now.AddDays(-7), (e) => {
        });

        public INC<DateTime> EndDateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });


        public StatisticOwnerViewModel(IStatisticController statisticController
                                      , IOwnerAuthService ownerAuthService
                                      , ISellerAuthService loginService
                                      ,ISellerOrderService sellerOrderService)
               :this(sellerOrderService)
        {
            this._statisticController = statisticController;
            this._ownerAuthService = ownerAuthService;
            this._loginService = loginService;
        }

        public StatisticOwnerViewModel(ISellerOrderService sellerOrderService) : base(sellerOrderService)
        {
        }

        public override void Start()
        {
            base.Start();
            this.Init();
        }

        private async void Init()
        {
            var user = await this._loginService.GetProfile();
        }
    }
}
