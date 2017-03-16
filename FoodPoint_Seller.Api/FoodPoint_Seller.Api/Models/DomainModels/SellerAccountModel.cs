 using System;
using Newtonsoft.Json;
namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class SellerAccountModel: AccountModel
    {
        public bool Busyness;
            
        public SellerAccountModel()
        {
        }

        public SellerAccountModel(string email, string password) : base(email, password)
        {
        }
    }
}
