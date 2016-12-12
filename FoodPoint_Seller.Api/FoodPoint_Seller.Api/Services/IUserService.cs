using FoodPoint_Seller.Api.Models.DomainModels;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
    public interface IUserService
    {
        Task<AccessTokenAuthorise> Authorization(UserModel user);
        Task<UserModel> GetProfileUser(int id, string token);
    }
}