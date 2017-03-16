using FoodPoint_Seller.Api.Models.DomainModels;
using Meowtrix.ITask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services
{
     public interface IAuth
    {
        Task<bool> Login();
        Task<bool> Login(string username, string password);
        void Logout();
        Task<string> GetToken();
        Task<AccountModel> GetProfile();
    }
}
