using FoodPoint_Seller.Api.Models.DomainModels;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services
{
    public interface IUserService
    {
        Task<AccessTokenAuthorise> AuthorizationSeller(SellerAccountModel user);

        Task<AccessTokenAuthorise> AuthorizationOwner(OwnerAccountModel user);
        Task<SellerAccountModel> GetProfileSeller(string id, string token);

        Task<OwnerAccountModel> GetProfileOwner(string id, string token);

        Task<string> Set_Busyness(string id, bool busyness, string token);
    }
}