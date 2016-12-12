using System;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using System.Threading.Tasks;
using Plugin.KeyChain.Abstractions;
using FoodPoint_Seller.Core.Services.Implementations;

namespace FoodPoint_Seller.Core.Services
{
    /// <summary>
    /// The login service.
    /// </summary>
    public class LoginService : ILoginService
    {
        private IUserController _userController;
        private readonly IKeyChain _keyChain;


        public const string KEY_LOGIN = "login";
        public const string KEY_PASSWORD = "password";

        private AccessTokenAuthorise _tokenAuth;
        private readonly DateTime START_UNIX_EPOH = new DateTime(1970, 1, 1, 0, 0, 0);


        private UserModel _profileUser;

        /// <summary>Initializes a new instance of the <see cref="LoginService"/> class.</summary>
        public LoginService(IUserController userController, IKeyChain keyChain) // e.g. LoginService(IMyApiClient client)
        {
            this._userController = userController;
            this._keyChain = keyChain;
        }

        public bool IsAuthenticated { get; private set; }

        public string ErrorMessage { get; private set; }

        public async Task<bool> Login()
        {
            var username = _keyChain.GetKey(LoginService.KEY_LOGIN);
            var password = _keyChain.GetKey(LoginService.KEY_PASSWORD);

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
                var user = new UserModel(username, password);
                _tokenAuth = await this._userController.Authorization(user);

                if (_tokenAuth != null)
                {
                    _keyChain.SetKey(LoginService.KEY_LOGIN, username);
                    _keyChain.SetKey(LoginService.KEY_PASSWORD, password);

                    IsAuthenticated = true;
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
            _profileUser = await _userController.GetProfileUser(
                Convert.ToInt32(Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token).sub),
                _tokenAuth.access_token
                );

            //_mvxMessenger.Publish(new UpdateProfileMessage(this));

            return true;
        }

        public UserModel GetProfileUser()
        {
            return _profileUser;
        }

    }
}