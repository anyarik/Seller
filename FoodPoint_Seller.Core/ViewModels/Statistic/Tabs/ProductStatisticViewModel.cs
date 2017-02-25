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
    public  class ProductStatisticViewModel : MvxViewModel
    {
        private IStatisticController _statisticController;
        private IOwnerAuthService _ownerAuthService;

        private ISellerAuthService _loginService;

        string formatsrc = "yyyy-MM-dd HH:mm:ss";
    
        //string formatdst = "dd:MM:yyyy";



        public INC<List<FoodDayInfo>> FoodStatisticListItem = new NC<List<FoodDayInfo>>(new List<FoodDayInfo>(), (e) =>
        {
        });

 
        public INC<DateTime> StartDateValue = new NC<DateTime>(DateTime.Now.AddDays(-7), (e) => {
        });

        public INC<DateTime> EndDateValue = new NC<DateTime>(DateTime.Now, (e) => {
        });


        public ProductStatisticViewModel(IStatisticController statisticController, IOwnerAuthService ownerAuthService, ISellerAuthService loginService)
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
            var token = await _ownerAuthService.GetToken();

            var startDate = StartDateValue.Value.ToString(formatsrc);
            var endDate = EndDateValue.Value.ToString(formatsrc);

            var foodStatistic = await _statisticController.GetFoodStatisticForDay(user.shopID.ToString(), startDate, endDate, token);
            FoodStatisticListItem.Value = foodStatistic;
        }

        
    }
}
