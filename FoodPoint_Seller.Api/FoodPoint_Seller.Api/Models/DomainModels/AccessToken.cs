using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Models.DomainModels
{
    public class AccessToken
    {
        public string sub { get; set; }
        /// <summary>
        /// Time unix epoh
        /// </summary>
        public int exp { get; set; }
    }
}
