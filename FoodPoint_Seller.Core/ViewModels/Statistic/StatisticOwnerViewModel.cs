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


        }
        
    }
}
