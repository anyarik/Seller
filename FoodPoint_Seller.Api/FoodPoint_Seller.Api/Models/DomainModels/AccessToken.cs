using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class AccessToken
    {
        public string client_id;
        public string scope;
        public dynamic amr;
        public dynamic iss;
        public string auth_time;
        public string role;
        public string aud;
        public string nbf;

        public string sub;
        /// <summary>
        /// Time unix epoh
        /// </summary>
        public int exp;
    }
}
