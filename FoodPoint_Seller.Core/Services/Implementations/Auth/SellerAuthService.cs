using System;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using System.Threading.Tasks;
using Plugin.KeyChain.Abstractions;
using FoodPoint_Seller.Core.Services.Implementations;
using Plugin.SecureStorage;
using Newtonsoft.Json;
using Polly;

namespace FoodPoint_Seller.Core.Services.Implementations
{
    /// <summary>
    /// The login service.
    /// </summary>
    public class SellerAuthService : BaseAuthService, ISellerAuthService
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

                _tokenAuth = await Policy.Handle<Exception>(_ => true)
                               .WaitAndRetryForeverAsync
                               (
                                   sleepDurationProvider: retry => TimeSpan.FromSeconds(10)
                               )
                               .ExecuteAsync(async () => await this._userController.AuthorizationSeller(user));


                if (_tokenAuth.access_token != null)
                {
                    //_keyChain.SetKey(KEY_LOGIN, username);
                    //_keyChain.SetKey(KEY_PASSWORD, password);

                    var isUpdate = await this.UpdateProfile();

                    return isUpdate ? IsAuthenticated = true : IsAuthenticated;
                }
                else
                {
                    return IsAuthenticated;
                }
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
                return true;
            else
                return false;
        }

        public  void ChangeStatusSeler()
        {
           _userController.Set_Busyness(_profileUser.ID, !(_profileUser as SellerAccountModel).Busyness, _tokenAuth.access_token);
           this.ChangeBusy((_profileUser as SellerAccountModel).Busyness);
        }
    }
}