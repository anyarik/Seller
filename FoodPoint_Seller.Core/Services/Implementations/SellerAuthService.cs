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
    public class SellerAuthService : ISellerAuthService
    {
        private IUserController _userController;
        private readonly IKeyChain _keyChain;

        public const string KEY_LOGIN = "login";
        public const string KEY_PASSWORD = "password";

        private AccessTokenAuthorise _tokenAuth;
        private readonly DateTime START_UNIX_EPOH = new DateTime(1970, 1, 1, 0, 0, 0);


        private SellerAccountModel _profileUser;

        /// <summary>Initializes a new instance of the <see cref="SellerAuthService"/> class.</summary>
        public SellerAuthService(IUserController userController, IKeyChain keyChain) // e.g. LoginService(IMyApiClient client)
        {
            this._userController = userController;
            this._keyChain = keyChain;
        }

        public bool IsAuthenticated { get; private set; }

        public string ErrorMessage { get; private set; }

        public async Task<bool> Login()
        {
            //var username = CrossSecureStorage.Current.GetValue(LoginService.KEY_LOGIN); 
            //var password = CrossSecureStorage.Current.GetValue(LoginService.KEY_PASSWORD);

            var username = _keyChain.GetKey(SellerAuthService.KEY_LOGIN);
            var password = _keyChain.GetKey(SellerAuthService.KEY_PASSWORD); 

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
               return await Login(username, password);
            }
            else
            {
                IsAuthenticated = false;

                return IsAuthenticated;
            }
        }

        /// <summary>The login method to retrieve OAuth2 access tokens from an API. </summary>
        /// <param name="userName">The user Name (email address) </param>
        /// <param name="password">The users <paramref name="password"/>. </param>
        /// <param name="scope">The required scopes. </param>
        /// <returns>The <see cref="bool"/>. </returns>
        public async Task<bool> Login(string username, string password)
        {
            try
            {
                var user = new SellerAccountModel(username, password);
                _tokenAuth = await this._userController.AuthorizationSeller(user);
                 
                if (_tokenAuth != null)
                {
                    _keyChain.SetKey(SellerAuthService.KEY_LOGIN, username);
                    _keyChain.SetKey(SellerAuthService.KEY_PASSWORD, password);
                    //CrossSecureStorage.Current.SetValue(LoginService.KEY_LOGIN, username);
                    //CrossSecureStorage.Current.SetValue(LoginService.KEY_PASSWORD, password);

                    var isUpdate = await  this.UpdateProfile();
                    
                    if (isUpdate)
                    {
                        IsAuthenticated = true;
                    }
                }

                return IsAuthenticated;
                //IsAuthenticated = _apiClient.ExchangeUserCredentialsForTokens(userName, password, scope);
            }
            catch (ArgumentException argex)
            {
                ErrorMessage = argex.Message;
                IsAuthenticated = false;

                return IsAuthenticated;
            }
        }
        public async Task<string> GetToken()
        {
            var accessToken = Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token);

            if (new DateTime(START_UNIX_EPOH.Ticks).AddSeconds(accessToken.exp) - DateTime.Now.ToUniversalTime() < new TimeSpan(0, 0, 5))
            {
                await this.Login();
            }

            return _tokenAuth.access_token;
            //throw new NotImplementedException();
        }

        private async Task<bool> UpdateProfile()
        {
             var id = Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token).sub.Replace(".seller", "");


            _profileUser = await  _userController.GetProfileSeller(
               id, _tokenAuth.access_token
                );

            //_mvxMessenger.Publish(new UpdateProfileMessage(this));
            if (_profileUser != null)
            {
                return true;
            }
            else
            {
                _profileUser = new SellerAccountModel(id, null, null,0);
                return true;
            }
            return false;
        }

        public async Task<SellerAccountModel> GetProfileSeller()
        {
            if (_profileUser.Equals(null))
            {
                var user = await this.UpdateProfile();

                if (user)
                {
                    return _profileUser;
                }
                else
                    return _profileUser;
            }
            return _profileUser;
        }

        public void Logout()
        {
            _keyChain.DeleteKey(OwnerAuthService.KEY_LOGIN);
            _keyChain.DeleteKey(OwnerAuthService.KEY_PASSWORD);
        }
    }
}