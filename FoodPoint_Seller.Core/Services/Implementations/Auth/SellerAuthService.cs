using System;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using System.Threading.Tasks;
using Plugin.KeyChain.Abstractions;
using FoodPoint_Seller.Core.Services.Implementations;
using Plugin.SecureStorage;
using Newtonsoft.Json;

namespace FoodPoint_Seller.Core.Services.Implementations
{
    /// <summary>
    /// The login service.
    /// </summary>
    public class SellerAuthService : BaseAuthService<SellerAccountModel>, ISellerAuthService
    {
        public SellerAuthService(IUserController userController, IKeyChain keyChain) : base(userController, keyChain)
        {
        }
        public async override Task<bool> Login(string username, string password)
        {
             //base.Login(username,password);
            try
            {
                var user = new SellerAccountModel(username, password);
                _tokenAuth = await this._userController.AuthorizationSeller(user);

                if (_tokenAuth != null)
                {
                    //_keyChain.SetKey(KEY_LOGIN, username);
                    //_keyChain.SetKey(KEY_PASSWORD, password);

                    var isUpdate = await this.UpdateProfile();

                    if (isUpdate)
                    {
                        IsAuthenticated = true;
                    }
                }

                return IsAuthenticated;
            }
            catch (ArgumentException argex)
            {
                ErrorMessage = argex.Message;
                IsAuthenticated = false;

                return IsAuthenticated;
            }
        }
        protected async override Task<bool> UpdateProfile()
        {
            var id = Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token).sub.Replace(".seller", "");

            _profileUser = await _userController.GetProfileSeller(
               id, _tokenAuth.access_token
                );

            if (_profileUser != null)
            {
                return true;
            }
            return false;
        }

        public async void ChangeStatusSeler()
        {
            _userController.Set_Busyness(_profileUser.ID, !_profileUser.IsBusy, _tokenAuth.access_token);
        }


    }
}