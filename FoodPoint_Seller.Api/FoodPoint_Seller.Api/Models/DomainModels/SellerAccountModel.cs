 using System;
using Newtonsoft.Json;
namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class SellerAccountModel
    {
        public SellerAccountModel(string email, string password)
        {
            this.EMail = email;
            this.Password = password;

        }

        public SellerAccountModel()
        {
            
        }

        public SellerAccountModel(string id, string name, string email, int shopID) 
        {
            this.EMail = email;
            this.ID = id;
            this.Name = name;
            this.shopID = shopID;
        }

        public string ID;

        public string Name;
        public string EMail;
        public int shopID;
        public string Password;

        public bool IsBusy;


    }
}
// "{\"ID\":\"72ad55f3-7cef-41ae-8091-3806e19a5053\",\"Name\":\"name\",\"EMail\":\"test@test.ru\",\"shopID\":1}"
// "{\"ID\":\"11\"                                  ,\"Name\":\"1\"   ,\"EMail\":\"1\"           ,\"shopID\":2}"