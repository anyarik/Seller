using FoodPoint_Seller.Api.Controllers;
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

        //public INC<List<SellerDayInfo>> SellerStatisticListItem = new NC<List<SellerDayInfo>>(new List<SellerDayInfo>(), (e) => {
        //});

        public INC<bool> IsFoodStatisticOpen = new NC<bool>(true, (e) => {
        });

        public INC<bool> IsSellerStatisticOpen = new NC<bool>(false, (e) => {
        });

        public INC<bool> IsOrderStatisticOpen = new NC<bool>(false, (e) => {
        });

        public StatisticOwnerViewModel(IStatisticController statisticController, IOwnerAuthService ownerAuthService)
        {
            this._statisticController = statisticController;
            this._ownerAuthService = ownerAuthService;

            
        }

        public override void Start()
        {
            base.Start();
            this.Init();
            //this.ListOrderItem.Value = _orderService.GetOrders();
        }

        private async void Init()
        {
            //var foodStatistic = _statisticController.GetSellerStatisticForDay();

        }

        public async void OnProductStatistic()
        {
            if (!IsFoodStatisticOpen.Value)
            {
                IsSellerStatisticOpen.Value = false;
                IsOrderStatisticOpen.Value = false;

                IsFoodStatisticOpen.Value = true;
                //var foodStatistic = _statisticController.GetSellerStatisticForDay();
            }
        }
        public async void OnSellerStatistic()
        {
            if (!IsSellerStatisticOpen.Value)
            {
                IsFoodStatisticOpen.Value = false;
                IsOrderStatisticOpen.Value = false;

                IsSellerStatisticOpen.Value = true;
                //var foodStatistic = _statisticController.GetSellerStatisticForDay();
            }
        }
        public async void OnOrderStatistic()
        {
            if (!IsOrderStatisticOpen.Value)
            {
                IsSellerStatisticOpen.Value = false;
                IsFoodStatisticOpen.Value = false;

                IsOrderStatisticOpen.Value = true;
                //var foodStatistic = _statisticController.GetSellerStatisticForDay();
            }
        }
    }
}
