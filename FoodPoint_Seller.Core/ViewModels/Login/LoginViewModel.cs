
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.Services.Implementations;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using Plugin.KeyChain.Abstractions;
using System;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ISellerAuthService _loginService;

        private readonly IDialogService _dialogService;

        public LoginViewModel(ISellerAuthService loginService, IDialogService dialogService)
        {
            _loginService = loginService;
            _dialogService = dialogService;

            Username.Value = "test@test.ru";
            Password.Value = "pp";
            IsLoading.Value = false;
        }

        public INC<string> Username = new NC<string>();

        public INC<string> Password = new NC<string>();

        public INC<bool> IsLoading = new NC<bool>();

        public async void Login()
        {
            try
            {
                var isLogin = await _loginService.Login(Username.Value, Password.Value);
                IsLoading.Value = true;
                if (isLogin)
                    ShowViewModel<MainViewModel>();

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
    }
}