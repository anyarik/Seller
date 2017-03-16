using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Core.Services;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services
{
    /// <summary>
    /// The LoginService <c>interface</c>.
    /// </summary>
    public interface ISellerAuthService: IAuth
    {
        void ChangeStatusSeler();
    }
}