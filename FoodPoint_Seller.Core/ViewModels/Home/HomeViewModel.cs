using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using FooodPoint_Seller.Core.Messeges;
using MvvmCross.Platform;
using System;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services.Implementations;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Api.Models.DomainModels;
using System.Collections.ObjectModel;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        //TODO Необходимо:
        // Групировать товары
        // Верстка
        // Диалоговые окна для дат
        // Занятость продавца
        // Таймер для оплаченного заказа
        // Просмотр полного заказа, при нажатии на него.

        public INC<string> TextActiveSeller = new NC<string>("Выйти", (e) =>
        {
        });

        public INC<string> TextStatusSeller = new NC<string>("offline", (e) =>
        {
        });

        public INC<string> OpenOrderNumber = new NC<string>("", (e) =>
        {
        });


        private IOrderController _orderController;
        private IUserController _userController;
        private ISellerOrderService _sellerOrderService;

        private ISellerAuthService _loginService;
        
        /// <summary> 
        /// Список заказов, который получены и согласованы
        /// </summary>
        public INC<List<PayedOrder>> ListOrderItem = new NC<List<PayedOrder>>(new List<PayedOrder>() { }, (e) =>
        {
        });

        public INC<bool> IsClikedOrderDialogOpen = new NC<bool>(false, (e) =>
        {
        });

        /// <summary>
        /// Список полученых текущих продуктов в пришедшем заказе
        /// </summary>
        public INC<List<ProductForOrder>> ListCurentOrderProductItem = new NC<List<ProductForOrder>>(new List<ProductForOrder>(), (e) =>
        {
        });

        public HomeViewModel(IOrderController orderController, IUserController userControler, ISellerAuthService loginService, ISellerOrderService sellerOrderService)
        {
            this._orderController = orderController;
            this._userController = userControler;

            this._loginService = loginService;
            this._sellerOrderService = sellerOrderService; 

            this._orderController.OnChangeStatusSeller((_, status) =>
            {
                TextStatusSeller.Value = status;
            });

        }
        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public override void Start()
        {
            base.Start();
            this.ListOrderItem.Value =  _sellerOrderService.GetOrders();

            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;
        }

        private void _sellerOrderService_OnNewPayedOrder(object sender, PayedOrder e)
        {
            this.ListOrderItem.Value.Add(e);
            UpdatePayedOrderList();
        }

        private void UpdatePayedOrderList()
        {
            var tempList = new List<PayedOrder>();
            foreach (var item in this.ListOrderItem.Value)
            {
                tempList.Add(item.Clone(item));
            }
            this.ListOrderItem.Value = tempList;
        }

        public void OrderClick(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = true;

            this.OpenOrderNumber.Value = order.Order.ID.ToString();
            this.ListCurentOrderProductItem.Value = order.Order.OrderedFood;
        }
        public void OnClose(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = false;
        }
        public async void OnClickOffline()
        {
            if (ListOrderItem.Value.Count == 0)
            {
                this._orderController.HubDisconnect();
                this._loginService.Logout();
                ShowViewModel<LoginViewModel>();
            }
            else
            {
                 this._loginService.ChangeStatusSeler();
                 TextActiveSeller.Value = "Больше закаты не принимаются";
            }
        }

        public void OnFinishOrder(PayedOrder deleteOrder)
        {
            this.ListOrderItem.Value.RemoveAll(o => o.Order.ID == deleteOrder.Order.ID);

            UpdatePayedOrderList();
            _sellerOrderService.DeletOrder(deleteOrder);
        }
    }
}