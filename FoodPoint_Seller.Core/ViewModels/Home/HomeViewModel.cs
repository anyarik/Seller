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
        // Загрузки при отправлении  запросов
        // Обновление информации при подключении к сигнаР
        // Просмотр заказа в отдельном окне
        // Верстка

        public INC<string> OpenOrderNumber = new NC<string>();
        public INC<bool> IsLoading = new NC<bool>();

        private readonly ISellerOrderService _sellerOrderService;
        private readonly ISellerAuthService _authService;

        private readonly IDialogService _dialogService;

        /// <summary> 
        /// Список заказов, который получены и согласованы
        /// </summary>
        public ObservableCollection<PayedOrder> ListOrderItem = new ObservableCollection<PayedOrder>();
        public INC<bool> IsClikedOrderDialogOpen = new NC<bool>(false);

        /// <summary>
        /// Список полученых текущих продуктов в пришедшем заказе
        /// </summary>
        public INC<List<ProductForOrder>> ListCurentOrderProductItem = new NC<List<ProductForOrder>>(new List<ProductForOrder>());

        private MvxSubscriptionToken _tokenClickOrder;

        public HomeViewModel( ISellerAuthService authService
                            , ISellerOrderService sellerOrderService
                            , IDialogService dialogService) 
            : this(sellerOrderService, authService)
        {
            this._authService = authService;
            this._sellerOrderService = sellerOrderService;
            this._dialogService = dialogService;

            this._tokenClickOrder = MvvmCross.Platform.Mvx.GetSingleton<IMvxMessenger>()
                                        .Subscribe<ClickOnFinishOrderMessage>(this.OnFinishOrder, MvxReference.Strong);

            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;
        }

        public HomeViewModel(ISellerOrderService sellerOrderService,  ISellerAuthService authService) 
            : base(sellerOrderService, authService)
        {
            
        }

        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public override async void Start()
        {
            var orders = new List<PayedOrder>();

            if (this.ListOrderItem.IsNullOrEmpty())
                orders = await _sellerOrderService.GetOrders();
            
            if (!orders.IsNullOrEmpty())
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

            this.ListOrderItem.RemoveItem(payedOrder);
            _sellerOrderService.DeletOrder(payedOrder.Order);
        }
    }
}