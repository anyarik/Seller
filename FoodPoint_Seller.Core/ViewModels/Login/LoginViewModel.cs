
using Acr.UserDialogs;
using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.Services.Implementations;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using Plugin.KeyChain.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly ISellerAuthService _loginService;
        private readonly IOwnerAuthService _ownerAuthService;
        private readonly IDialogService _dialogService;
        private readonly IUserDialogs _userDialogs;

        public INC<string> Username = new NC<string>();
        public INC<string> Password = new NC<string>();
        public INC<bool> IsLoading = new NC<bool>();

        public INC<List<string>> RoleList;
        //public INC<MyEnum> CurrentRole = new NC<MyEnum>();
        public INC<string> CurrentRole = new NC<string>("");

        public  readonly  string sellerRole = "Продавец";
        public readonly string chiefRole = "Управляющий";
        public enum RolesEnum
        {
            Продавец,
            Управляющий
        }

        public LoginViewModel(ISellerAuthService loginService
                             , IDialogService dialogService
                             , IOwnerAuthService ownerAuthService 
                             , IUserDialogs userDialogs )
        {
            this._loginService = loginService;
            this._dialogService = dialogService;
            this._ownerAuthService = ownerAuthService;

            this._userDialogs = userDialogs;

            this.Username.Value = "test@test.ru";
            this.Password.Value = "pp";
            this.IsLoading.Value = false;

            this.RoleList = new NC<List<string>>(new List<string>(){RolesEnum.Продавец.ToString()
                                                                    , RolesEnum.Управляющий.ToString()
                                                                    , "Выберете роль" });

            CurrentRole.Changed += CurrentRole_Changed;
        }

        private void CurrentRole_Changed(object sender, EventArgs e)
        {
           
        }

        public async void Login()
        {
            try
            {
                if (CurrentRole.Value == RolesEnum.Продавец.ToString())
                {
                    roleLog<MainViewModel>(_loginService);
                }
                else if (CurrentRole.Value == RolesEnum.Управляющий.ToString())
                {
                    roleLog<StatisticOwnerViewModel>(_ownerAuthService);
                }
                else
                {
                    var toastConfig = new ToastConfig("Выберете роль");
                    toastConfig.BackgroundColor = Color.DarkRed;
                    this._userDialogs.Toast(toastConfig);
                }
            }
 	        catch (System.Exception )
	        {
                throw new Exception("Ошибка в авторизации");
            }
        }
        private async void roleLog<TModel>(IAuth authServise)
            where TModel : IMvxViewModel
        {
            _userDialogs.Loading("Загрузка", null, null, true, MaskType.Gradient);
            var isLogin = await authServise.Login(Username.Value, Password.Value);

            if (isLogin)
            {
                _userDialogs.Loading("Загрузка").Hide(); ;
                ShowViewModel<TModel>();
            }
            else
            {
                _userDialogs.Loading("Загрузка").Hide();
                _userDialogs.Alert("Ошибка авторизации", "Неправильный логин или пароль");
            }
        } 
    }
}