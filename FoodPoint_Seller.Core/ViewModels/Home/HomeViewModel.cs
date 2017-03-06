using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.FieldBinding;
using System.Collections.Generic;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services;
using FoodPoint_Seller.Core.ViewModels.Base;

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

        public HomeViewModel(IOrderController orderController
                           , IUserController userControler
                           , ISellerAuthService loginService
                           , ISellerOrderService sellerOrderService) 
            : this(sellerOrderService)
        {
            this._orderController = orderController;
            this._userController = userControler;

            this._loginService = loginService;
            this._sellerOrderService = sellerOrderService;

            this.ListOrderItem.Changed += ListOrderItem_Changed;
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
            }

            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;
        }

        public void ShowMenu()
        {
            ShowViewModel<MenuViewModel>();
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

        public void OnFinishOrder(PayedOrder deleteOrder)
        {
            this.ListOrderItem.Value.RemoveAll(o => o.Order.ID == deleteOrder.Order.ID);

            UpdatePayedOrderList();
            _sellerOrderService.DeletOrder(deleteOrder);
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