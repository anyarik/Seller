using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Base;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public  class StatisticOwnerViewModel: MvxViewModel
    {
        //private IStatisticController _statisticController;
        //private IOwnerAuthService _ownerAuthService;
        
        //protected OwnerAccountModel User;
    
        public StatisticOwnerViewModel()
        {

        }

        public void OnClickExit()
        {
            this.Close(this);
            ShowViewModel<LoginViewModel>();
        }

        //public StatisticOwnerViewModel(ISellerOrderService sellerOrderService) : base(sellerOrderService)
        //{
        //}

        //public override void Start()
        //{
        //    base.Start();
        //    this.Init();
        //}

        //private async void Init()
        //{
        //   // var user = await this._ownerAuthService.GetProfile();
        //}
    }
}
