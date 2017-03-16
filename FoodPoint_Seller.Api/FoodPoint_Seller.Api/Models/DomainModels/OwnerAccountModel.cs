using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class OwnerAccountModel: AccountModel
    {
        public OwnerAccountModel(string email, string password) : base(email, password)
        {

        }
    }
}
