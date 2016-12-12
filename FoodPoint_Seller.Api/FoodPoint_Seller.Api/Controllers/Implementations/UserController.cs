using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Api.Services;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Controllers
{
    public  class UserController: IUserController
    {
        private IUserService userService;
 

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<AccessTokenAuthorise> Authorization(UserModel user) => await userService.Authorization(user);

        public async Task<UserModel> GetProfileUser(int id, string token) => await userService.GetProfileUser(id, token);

    }
}
