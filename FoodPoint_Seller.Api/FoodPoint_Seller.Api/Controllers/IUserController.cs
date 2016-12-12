using FoodPoint_Seller.Api.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Controllers
{
    public interface IUserController
    {
        Task<AccessTokenAuthorise> Authorization(UserModel user);

        Task<UserModel> GetProfileUser(int id, string token);
    }
}
