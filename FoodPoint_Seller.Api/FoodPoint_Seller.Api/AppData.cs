using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller
{
    public static class AppData
    {
        public static string Host
        {
            get { return "http://193.124.66.1:50387"; }
        }
        public static string Identity
        {
            get { return "http://193.124.66.1:55493"; }
        }
        public static string YaSuccessUri
        {
            get { return "http://tapki.azurewebsites.net/yandex/success"; }
        }

        public static string YaFailUri
        {
            get { return "http://tapki.azurewebsites.net/yandex/fail"; }
        }
    }
}
