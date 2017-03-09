using Acr.UserDialogs;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Controllers.Implementations;
using FoodPoint_Seller.Api.Services;
using FoodPoint_Seller.Api.Services.Implementations;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.Services.Implementations;
using FoodPoint_Seller.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using MvvmCross.Plugins.Messenger;
using Plugin.KeyChain.Abstractions;

namespace FoodPoint_Seller.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            // Registers any classes ending with "Service" into the internal
            // Mvx IoC container for use when constructing objects through
            // the container
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            Mvx.RegisterSingleton<IKeyChain>(() => Plugin.KeyChain.CrossKeyChain.Current);

            Mvx.RegisterSingleton<IOrderService>(new OrderService());
            Mvx.RegisterSingleton<IUserService>(new UserService());
            Mvx.RegisterSingleton<IStatisticService>(new StatisticService());
            Mvx.RegisterSingleton<IOrderHubService>(new OrderHubService());

            Mvx.ConstructAndRegisterSingleton<IUserController, UserController>();
            Mvx.ConstructAndRegisterSingleton<IOrderController, OrderController>();
            Mvx.ConstructAndRegisterSingleton<IStatisticController, StatisticControler>();

            Mvx.RegisterSingleton<IUserDialogs>(() => UserDialogs.Instance);


            // Construct custom application start object
            Mvx.ConstructAndRegisterSingleton<IMvxAppStart, AppStart>();

            // request a reference to the constructed appstart object 
            var appStart = Mvx.Resolve<IMvxAppStart>();

            // register the appstart object
            RegisterAppStart(appStart);
        }
    }
}