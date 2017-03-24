using Acr.UserDialogs;
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
        readonly IUserDialogs _userArcDialogs;

        public StatisticOwnerViewModel(IUserDialogs userArcDialogs)
        {
            this._userArcDialogs = userArcDialogs;

            _userArcDialogs.Loading("Загрузка").Hide();
        }

        public void OnClickExit()
        {
            this.Close(this);
            ShowViewModel<LoginViewModel>();
        }
    }
}
