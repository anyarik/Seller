using MvvmCross.Platform.Platform;
using System;
using System.Diagnostics;
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
            catch (System.Exception a)
            {
                Debug.WriteLine("Error", "Ошибка", a.Message);
                throw new Exception(a.Message);
            }
           
        }
    }
}