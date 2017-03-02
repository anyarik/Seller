using FoodPoint_Seller.Api.Controllers;
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
    public class LoginOwnerViewModel: BaseFragment
    {
        private readonly IOwnerAuthService _ownerAuthService;
        private readonly IDialogService _dialogService;

        public LoginOwnerViewModel(IOwnerAuthService ownerAuthService
                                 , IDialogService dialogService
                                 , ISellerOrderService sellerOrderService)
            :base(sellerOrderService)
        {
            _ownerAuthService = ownerAuthService;
            _dialogService = dialogService;

            Username.Value = "test@test.ru";
            Password.Value = "pp";

            this.IsLoading.Value = false;
        }

        public INC<string> Username = new NC<string>();

        public INC<string> Password = new NC<string>();

        public INC<bool> IsLoading = new NC<bool>();

        public async void Login()
        {
            try
            {
                var isLogin = await _ownerAuthService.Login(Username.Value, Password.Value);
                IsLoading.Value = true;
                if (isLogin)
                    ShowViewModel<StatisticOwnerViewModel>();

                else
                {
                    IsLoading.Value = false;
                    _dialogService.Alert("We were unable to log you in!", "Login Failed", "OK");
                }
            }

            catch (System.Exception a)
            {
                throw new Exception("Ошибка в авторизации");
            }
        }

        public override void Start()
        {
            base.Start();
            this.Init();
            //this.ListOrderItem.Value = _orderService.GetOrders();
        }

        private async void Init()
        {
        }
    }
}
