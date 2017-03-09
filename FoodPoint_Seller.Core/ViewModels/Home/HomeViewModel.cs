using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.FieldBinding;
using System.Collections.Generic;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Base;
using System;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;
using System.Collections.ObjectModel;
using MvvmCross.Platform.Core;
using System.Collections;
using FoodPoint_Seller.Core.Extentions;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class HomeViewModel : BaseFragment
    {
        //TODO Необходимо:
        // Групировать товары
        // Верстка
        // Занятость продавца

        public INC<string> OpenOrderNumber = new NC<string>("", (e) =>
        {
        });


        private IOrderController _orderController;
        private IUserController _userController;
        private ISellerOrderService _sellerOrderService;

        private ISellerAuthService _loginService;
        private IDialogService _dialogService;

        /// <summary> 
        /// Список заказов, который получены и согласованы
        /// </summary>
        public ObservableCollection<PayedOrder> ListOrderItem = new ObservableCollection<PayedOrder>();
        //(new ObservableCollection<PayedOrder>() { }, (e) =>
        //{
        //});

        public INC<bool> IsClikedOrderDialogOpen = new NC<bool>(false, (e) =>
        {
        });

        /// <summary>
        /// Список полученых текущих продуктов в пришедшем заказе
        /// </summary>
        public INC<List<ProductForOrder>> ListCurentOrderProductItem = new NC<List<ProductForOrder>>(new List<ProductForOrder>(), (e) =>
        {
        });

        private MvxSubscriptionToken tokenClickOrder;

        public HomeViewModel(IOrderController orderController
                           , IUserController userControler
                           , ISellerAuthService loginService
                           , ISellerOrderService sellerOrderService
                           , IDialogService dialogService) 
            : this(sellerOrderService)
        {
            this._orderController = orderController;
            this._userController = userControler;

            this._loginService = loginService;
            this._sellerOrderService = sellerOrderService;
            this._dialogService = dialogService;

            this.tokenClickOrder = MvvmCross.Platform.Mvx.GetSingleton<IMvxMessenger>().Subscribe<ClickOnFinishOrderMessage>(this.OnFinishOrder, MvxReference.Strong);
            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;
        }


        public HomeViewModel(ISellerOrderService sellerOrderService) : base(sellerOrderService)
        {

        }
        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public async override void Start()
        {
            List<PayedOrder> orders = new List<PayedOrder>();

            if (this.ListOrderItem.Count == 0)
                orders = await _sellerOrderService.GetOrders();


            if (orders.Count != 0 )
            {
                foreach (var item in orders)
                {
                    MvxMainThreadDispatcher.Instance.RequestMainThreadAction(() => this.ListOrderItem.Add(item));
                }
            }
            base.Start();
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
        }

        private void _sellerOrderService_OnNewPayedOrder(object sender, PayedOrder addOrder)
        {
            _dialogService.Notification(new NotificaiosModel($"Заказ №{addOrder.Order.RowNumber} оплачен"
                                                , "Начинайте готовить")
                           );
            MvxMainThreadDispatcher.Instance.RequestMainThreadAction(() => this.ListOrderItem.Add(addOrder));
        }


        public void OrderClick(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = true;

            this.OpenOrderNumber.Value = order.Order.RowNumber.ToString();
            this.ListCurentOrderProductItem.Value = order.Order.OrderedFood;
        }
        public void OnClose()
        {
            this.IsClikedOrderDialogOpen.Value = false;
        }

        public void OnFinishOrder(ClickOnFinishOrderMessage payed)
        {
            var payedOrder = payed.Sender as PayedOrder;

            if (payedOrder == null) return;

            //foreach (var item in this.ListOrderItem)
            //{
            //    if (item.Order.ID == payedOrder.Order.ID)
            //    {
            //        this.ListOrderItem.Remove(item);
            //    }
            //}
            this.ListOrderItem.RemoveItem(payedOrder);
            _sellerOrderService.DeletOrder(payedOrder.Order);
        }

        public  void OnClickOffline()
        {
            //if (ListOrderItem.Value.Count == 0)
            //{
            //    this._orderController.HubDisconnect();
            //    this._loginService.Logout();
            //    ShowViewModel<LoginViewModel>();
            //}
            //else
            //{
            //    this._loginService.ChangeStatusSeler();
            //    TextActiveSeller.Value = "Больше заказы не принимаются";
            //}
        }
    }
}