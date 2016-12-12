using System;
using UIKit;

namespace FoodPoint_Seller.Touch
{
    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            try
            {
                UIApplication.Main(args, null, "AppDelegate");
            }
            catch (System.Exception)
            {

                throw new Exception("Ошибка в загрузке приложения");
            }
           
        }
    }
}