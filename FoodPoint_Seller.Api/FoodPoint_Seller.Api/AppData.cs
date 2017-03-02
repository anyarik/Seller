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
            get { return "http://95.46.44.224:50387"; }
        }
        public static string Identity
        {
            get { return "http://95.46.44.224:55493"; }
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
