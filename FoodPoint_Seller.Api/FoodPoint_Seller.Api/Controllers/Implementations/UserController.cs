using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Api.Services;
using System.Threading.Tasks;
using System;

namespace FoodPoint_Seller.Api.Controllers.Implementations
{
    public  class UserController: IUserController
    {
        private IUserService _userService;
 

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        public async Task<AccessTokenAuthorise> AuthorizationOwner(OwnerAccountModel user)  => await _userService.AuthorizationOwner(user);
        public async Task<AccessTokenAuthorise> AuthorizationSeller(SellerAccountModel user) => await _userService.AuthorizationSeller(user);

        public async Task<OwnerAccountModel> GetProfileOwner(string id, string token) => await _userService.GetProfileOwner(id, token);
        public async Task<SellerAccountModel> GetProfileSeller(string id, string token) => await _userService.GetProfileSeller(id, token);

    }
}
