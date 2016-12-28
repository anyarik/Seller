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
    public class OwnerAuthService : IOwnerAuthService
    {
        private IUserController _userController;
        private readonly IKeyChain _keyChain;


        public const string KEY_LOGIN = "login_owner";
        public const string KEY_PASSWORD = "password_owner";

        private AccessTokenAuthorise _tokenAuth;
        private readonly DateTime START_UNIX_EPOH = new DateTime(1970, 1, 1, 0, 0, 0);


        private OwnerAccountModel _profileUser;

        /// <summary>Initializes a new instance of the <see cref="LoginService"/> class.</summary>
        public OwnerAuthService(IUserController userController, IKeyChain keyChain) // e.g. LoginService(IMyApiClient client)
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

            var username = _keyChain.GetKey(OwnerAuthService.KEY_LOGIN);
            var password = _keyChain.GetKey(OwnerAuthService.KEY_PASSWORD); 

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                IsAuthenticated = true;

                //return IsAuthenticated;
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
                var user = new OwnerAccountModel(username, password);
                _tokenAuth = await this._userController.AuthorizationOwner(user);
                 
                if (_tokenAuth != null)
                {
                    _keyChain.SetKey(OwnerAuthService.KEY_LOGIN, username);
                    _keyChain.SetKey(OwnerAuthService.KEY_PASSWORD, password);
                    //CrossSecureStorage.Current.SetValue(LoginService.KEY_LOGIN, username);
                    //CrossSecureStorage.Current.SetValue(LoginService.KEY_PASSWORD, password);

                    var isUpdate = true;//await  this.UpdateProfile();

                    if (isUpdate)
                    {
                        IsAuthenticated = true;
                    }

                    
                }
             
                //_mvxMessenger.Publish(new AuthorizationMessage(this));

                //await UpdateProfile();

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


            _profileUser = await  _userController.GetProfileOwner(
               id, _tokenAuth.access_token
                );

            //_mvxMessenger.Publish(new UpdateProfileMessage(this));
            if (_profileUser != null)
            {
                return true;
            }
            return false;
        }

        public async Task<OwnerAccountModel> GetProfileOwner()
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