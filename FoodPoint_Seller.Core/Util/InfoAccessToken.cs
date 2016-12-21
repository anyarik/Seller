using FoodPoint_Seller.Api.Models.DomainModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Util
{
    public static class InfoAccessToken
    {
        public static AccessToken GetInfoFromToken(string token)
        {

            string[] data = token.Split(new char[] { '.' });

            var d4 = data[1].Length % 4;
            if (d4 > 0)
            {
                data[1] = data[1].PadRight(data[1].Length + (4 - d4), '=');
            }

            byte[] byt = System.Convert.FromBase64String(data[1]);

            var str = Encoding.UTF8.GetString(byt, 0, byt.Length);
            try
            {

                return JsonConvert.DeserializeObject<AccessToken>(str);
            }
            catch (Exception a)
            {
                return null;
            }

       
        }
    }
}
