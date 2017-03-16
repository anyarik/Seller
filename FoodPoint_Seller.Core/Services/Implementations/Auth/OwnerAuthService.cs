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
    public class OwnerAuthService : BaseAuthService,  IOwnerAuthService
    {
        public OwnerAuthService(IUserController userController, IKeyChain keyChain) : base(userController, keyChain)
        {

        }

        public async override  Task<bool> Login(string username, string password)
        {
            try
            {
                var user = new OwnerAccountModel(username, password);

                _tokenAuth = await Policy.Handle<Exception>(_ => true)
                                       .WaitAndRetryForeverAsync
                                       (
                                           sleepDurationProvider: retry => TimeSpan.FromSeconds(10)
                                       )
                                       .ExecuteAsync(async () => await this._userController.AuthorizationOwner(user));


                if (_tokenAuth.access_token != null)
                {
                    var isUpdate = await this.UpdateProfile();

                    if (isUpdate)
                        return IsAuthenticated = true;
                    else
                        return IsAuthenticated;

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
            var id = Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token).sub.Replace(".chief", "");

            _profileUser = await _userController.GetProfileOwner(
               id, _tokenAuth.access_token
                );

            if (_profileUser != null)
            {
                return true;
            }

            return true;
        }
    }
}