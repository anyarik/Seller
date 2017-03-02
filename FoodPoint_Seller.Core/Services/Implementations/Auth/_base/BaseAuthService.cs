using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using Plugin.KeyChain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services.Implementations
{
    public abstract class BaseAuthService<T>
    {
        protected IUserController _userController;
        protected readonly IKeyChain _keyChain;
        
        public const string KEY_LOGIN = "login";
        public const string KEY_PASSWORD = "password";

        protected AccessTokenAuthorise _tokenAuth;
        private readonly DateTime START_UNIX_EPOH = new DateTime(1970, 1, 1, 0, 0, 0);
        
        protected T _profileUser;
        protected bool IsAuthenticated { get; set; }
        protected string ErrorMessage { get; set; }

        /// <summary>Initializes a new instance of the <see cref="LoginService"/> class.</summary>
        public BaseAuthService(IUserController userController, IKeyChain keyChain)
        {
            this._userController = userController;
            this._keyChain = keyChain;
        }


        public async Task<bool> Login()
        {
            var username = _keyChain.GetKey(KEY_LOGIN);
            var password = _keyChain.GetKey(KEY_PASSWORD);

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                IsAuthenticated = true;
                return await Login(username, password);
            }
            else
            {
                IsAuthenticated = false;
                return IsAuthenticated;
            }
        }

        public  abstract Task<bool> Login(string username, string password);

        protected abstract Task<bool> UpdateProfile();

        public async Task<string> GetToken()
        {

            var accessToken = Util.InfoAccessToken.GetInfoFromToken(_tokenAuth.access_token);

            if (new DateTime(START_UNIX_EPOH.Ticks).AddSeconds(accessToken.exp) - DateTime.Now.ToUniversalTime() < new TimeSpan(0, 0, 5))
            {
                await this.Login();
            }

            return _tokenAuth.access_token;
        }
        public virtual async Task<T> GetProfile()
        {
            if (_profileUser.Equals(null))
            {
                var user = await this.UpdateProfile();

                if (user)
                {
                    return _profileUser;
                }
            }
            return _profileUser;
        }
        public void Logout()
        {
            _keyChain.DeleteKey(KEY_LOGIN);
            _keyChain.DeleteKey(KEY_PASSWORD);
        }
    }
}
