using FoodPoint_Seller.Api.Models.DomainModels;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services
{
    /// <summary>
    /// The LoginService <c>interface</c>.
    /// </summary>
    public interface ISellerAuthService
    {

        /// <summary>
        /// Attempts to log the user in using stored credentials if present
        /// </summary>
        /// <returns> <c>true</c> if the login is successful, <c>false</c> otherwise </returns>
        Task<bool> Login();

        /// <summary>The login method to retrieve OAuth2 access tokens from an API. </summary>
        /// <param name="userName">The user Name (email address) </param>
        /// <param name="password">The users <c>password</c>. </param>
        /// <returns><c>true</c> if the login is successful, <c>false</c> otherwise </returns>
        Task<bool> Login(string username, string password);

        Task<string> GetToken();
        void ChangeStatusSeler();

        Task<SellerAccountModel> GetProfileSeller();
        void Logout();
    }
}