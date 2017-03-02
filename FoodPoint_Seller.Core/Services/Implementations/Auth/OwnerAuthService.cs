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
    public class OwnerAuthService : BaseAuthService<OwnerAccountModel>, IOwnerAuthService
    {
        public OwnerAuthService(IUserController userController, IKeyChain keyChain) : base(userController, keyChain)
        {

        }

        public async override  Task<bool> Login(string username, string password)
        {
            try
            {
                var user = new OwnerAccountModel(username, password);
                _tokenAuth = await this._userController.AuthorizationOwner(user);

                if (_tokenAuth != null)
                {
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
            var id = Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token).sub.Replace(".chief", "");

            //_profileUser = await _userController.GetProfileOwner(
            //   id, _tokenAuth.access_token
            //    );

            //if (_profileUser != null)
            //{
            //    return true;
            //}

            return true;
        }
    }
}