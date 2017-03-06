using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.FieldBinding;
using System.Collections.Generic;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Base;
using System;
using MvvmCross.Plugins.Messenger;

namespace FoodPoint_Seller.Core.ViewModels
{
    public class HomeViewModel : BaseFragment
    {
        //TODO Необходимо:
        // Групировать товары
        // Верстка
        // Занятость продавца
        // Таймер для оплаченного заказа
        // Просмотр полного заказа, при нажатии на него.


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

            this.ListOrderItem.Changed += ListOrderItem_Changed;
            this.tokenClickOrder = MvvmCross.Platform.Mvx.GetSingleton<IMvxMessenger>().Subscribe<ClickOnFinishOrderMessage>(this.OnFinishOrder, MvxReference.Strong);
            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;


        }

        private void ListOrderItem_Changed(object sender, System.EventArgs e)
        {
            var a = this.ListOrderItem.Value;
        }

        public HomeViewModel(ISellerOrderService sellerOrderService) : base(sellerOrderService)
        {

        }
        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public override void Start()
        {
            base.Start();
            var orders = _sellerOrderService.GetOrders();
            if (orders.Count != 0)
            {
                this.ListOrderItem.Value = _sellerOrderService.GetOrders();
                foreach (var orderItem in this.ListOrderItem.Value)
                {
                    orderItem.func = (orderInProcess) =>
                      {
                          orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, (_) =>
                          {
                              orderInProcess.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);
                              UpdatePayedOrderList();
                              if (orderInProcess.CloseOrderTimer.WaitTime == TimeSpan.Zero)
                              {
                                  _dialogService.Notification(new NotificaiosModel($"Время приготовления заказа №{orderInProcess.Order.RowNumber} закончилось"
                                                                        , ""));


                                  orderInProcess.StopTimer();
                                  orderInProcess.IsOrderFisihed = true;
                              }
                          });
                      };
                    orderItem.StartTimer();
                }
            }
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

            addOrder.func = (orderInProcess) =>
            {
                orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, (_) =>
                {
                    orderInProcess.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);
                    if (orderInProcess.CloseOrderTimer.WaitTime == TimeSpan.Zero)
                    {
                        _dialogService.Notification(new NotificaiosModel($"Время приготовления заказа №{addOrder.Order.RowNumber} закончилось"
                                                                        , "")
                                                   );
                        orderInProcess.StopTimer();
                        orderInProcess.IsOrderFisihed = true;
                    }
                    UpdatePayedOrderList();
                });
            };
            if (addOrder != null)
            {
                addOrder.StartTimer();
            }
            this.ListOrderItem.Value.Add(addOrder);
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

            this.ListOrderItem.Value.RemoveAll(o => o.Order.ID == payedOrder.Order.ID);

            UpdatePayedOrderList();
            _sellerOrderService.DeletOrder(payedOrder.Order);
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
                TextActiveSeller.Value = "Больше заказы не принимаются";
            }
        }
    }
}