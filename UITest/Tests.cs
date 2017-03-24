using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace UITest
{
    [TestFixture(Platform.Android)]
   // [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app =  ConfigureApp
                    .Android
                    .ApkFile("../../../FoodPoint_Seller.Droid/bin/Debug/ru.FoodPoint_Seller-Signed.apk")
                    .StartApp();
        }

        [Test]
        public void AppLaunches()
        {
            app.Screenshot("First screen.");
        }

        [Test]
        public void NewTest()
        {
            app.Tap(x => x.Text("Выберете роль"));
            app.Tap(x => x.Text("Продавец"));
            app.Tap(x => x.Text("Войти"));
            app.Tap(x => x.Text("Выйти"));
        }
    }
}

