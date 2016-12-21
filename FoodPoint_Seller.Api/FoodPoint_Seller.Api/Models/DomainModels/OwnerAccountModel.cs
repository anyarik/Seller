using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class OwnerAccountModel
    {
        public OwnerAccountModel(string email, string password)
        {
            this.EMail = email;
            this.Password = password;

        }

        public OwnerAccountModel()
        {

        }

        public OwnerAccountModel(string id, string name, string email, int shopID)
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
    }
}
