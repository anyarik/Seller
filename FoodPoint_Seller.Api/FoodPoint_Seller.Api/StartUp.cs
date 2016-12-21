using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Controllers.Implementations;
using FoodPoint_Seller.Api.Services;
using FoodPoint_Seller.Api.Services.Implementations;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using System;

namespace FoodPoint_Seller
{
    public class StartUp
    {
    
        public static void Start() => Configurate();

        private static void Configurate()
        {
            ////здесь будет конфигурироваться DI контейнер
            MvxSimpleIoCContainer.Initialize();


            Mvx.RegisterSingleton<IOrderService>(new OrderService());
            Mvx.RegisterSingleton<IUserService>(new UserService());
            Mvx.RegisterSingleton<IOrderHubService>(new OrderHubService());

            Mvx.ConstructAndRegisterSingleton<IUserController, UserController>();
            Mvx.ConstructAndRegisterSingleton<IOrderController, OrderController>();
        }

    } 
}

