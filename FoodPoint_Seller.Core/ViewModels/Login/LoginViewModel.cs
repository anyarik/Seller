
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
        private readonly IUserDialogs _userArcDialogs;

        public INC<string> Username = new NC<string>();
        public INC<string> Password = new NC<string>();

        public INC<List<string>> RoleList;
        //public INC<MyEnum> CurrentRole = new NC<MyEnum>();
        public INC<string> CurrentRole = new NC<string>("");

        public enum RolesEnum
        {
            Продавец,
            Управляющий
        }

        public LoginViewModel(ISellerAuthService loginService
                             , IOwnerAuthService ownerAuthService 
                             , IUserDialogs userArcDialogs )
        {
            this._loginService = loginService;
            this._ownerAuthService = ownerAuthService;

            this._userArcDialogs = userArcDialogs;

            this.Username.Value = "test@test.ru";
            this.Password.Value = "pp";

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
                if(CurrentRole.Value.Equals(RolesEnum.Продавец.ToString()))
                {
                   roleLog<MainViewModel>(_loginService);
                }
                else if(CurrentRole.Value.Equals(RolesEnum.Управляющий.ToString()))
                {
                    roleLog<StatisticOwnerViewModel>(_ownerAuthService);
                }
                else
                {
                    var toastConfig = new ToastConfig("Выберете роль!")
                    {
                        BackgroundColor = Color.DarkRed
                    };
                    this._userArcDialogs.Toast(toastConfig);
                }
            }
 	        catch(System.Exception)
	        {
                _userArcDialogs.Loading().Hide();
                _userArcDialogs.Alert("Ошибка авторизации", "Неправильный логин или пароль");
            }
        }
        private async void roleLog<TModel>(IAuth authServise)
            where TModel : IMvxViewModel
        {
             _userArcDialogs.Loading("Загрузка");
            var isLogin = await authServise.Login(Username.Value, Password.Value);

            if (isLogin)
            {
                ShowViewModel<TModel>();
            }
            else
            {
                _userArcDialogs.Loading().Hide();
                _userArcDialogs.Alert("Ошибка авторизации", "Неправильный логин или пароль");
            }
        } 
    }
}