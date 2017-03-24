using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .ApkFile("../../../FoodPoint_Seller.Droid/bin/Debug/ru.FoodPoint_Seller-Signed.apk")
                    .StartApp();
            }

            return ConfigureApp
                .iOS
                .AppBundle("../../../FoodPoint_Seller.iOS/bin/iPhoneSimulator/Debug/FoodPoint_Seller.Touch.app")
                .StartApp();
        }
    }
}

