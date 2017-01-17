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

namespace FoodPoint_Seller.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        //TODO Необходимо:
        // Получать текущие активные заказы магазина на входе
         // Таймер для оплаченного заказа
        // Выключать таймер когда заказ отправлен и запускать новый под новый этап.
        // Просмотр полного заказа, при нажатии на него.

        public INC<string> TextActiveSeller = new NC<string>("Выйти", (e) =>
        {
        });

        public INC<string> TextStatusSeller = new NC<string>("offline", (e) =>
        {
        });
        
        private IOrderController _orderController;
        private IUserController _userController;

        private ISellerAuthService _loginService;
        
        /// <summary> 
        /// Список заказов, который получены и согласованы
        /// </summary>
        public INC<List<PayedOrder>> ListOrderItem = new NC<List<PayedOrder>>(new List<PayedOrder>(), (e) =>
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

        public HomeViewModel(IOrderController orderController, IUserController userControler, ISellerAuthService loginService)
        {
            this._orderController = orderController;
            this._userController = userControler;

            this._loginService = loginService;

            this._orderController.OnChangeStatusSeller((_, status) =>
            {
                TextStatusSeller.Value = status;
            });

            this._orderController.OnGettingPurchasedOrders((custormer, order) =>
            {
                var reciveOrder = JsonConvert.DeserializeObject<OrderItem>(order);

                var showingOrder = new PayedOrder(custormer.ToString(), reciveOrder, reciveOrder.orderTimer, (orderInProcess) =>
                {
                    orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, (_) =>
                    {
                        orderInProcess.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);

                        if (orderInProcess.CloseOrderTimer.WaitTime == TimeSpan.Zero)
                        {
                            orderInProcess.IsOrderFisihed = true;
                        }
                    });

                });


                //this.ListOrderItem.Value.Add(showingOrder);
                //var tempList = new List<PayedOrder>();
                //foreach (var item in this.ListOrderItem.Value)
                //{
                //    tempList.Add(item.Clone(item));
                //}
                //this.ListOrderItem.Value = tempList;

                TextActiveSeller.Value = "Перестать принимать заказы";
            });
        }
        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public override void Start()
        {
            base.Start();
           //this.ListOrderItem.Value = _orderService.GetOrders();
        }


        public void OrderClick(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = true;

            //this.RecivedOrderNumber.Value = order.Order.ID.ToString();
            this.ListCurentOrderProductItem.Value = order.Order.OrderedFood;
        }
        public void OnClose(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = false;
        }
        public void onClickOffline()
        {
            if (ListOrderItem.Value.Count == 0)
            {
                this._orderController.HubDisconnect();
                ShowViewModel<LoginViewModel>();
            }
            else
            {
                //this._userController.Set_Busyness();
            }
           
        }

        public void OnFinishOrder(PayedOrder deleteOrder)
        {
            //this.ListOrderItem.Value.RemoveAll(o => o.Order.ID == deleteOrder.Order.ID);

            //var tempList = new List<PayedOrder>();
            //foreach (var item in this.ListOrderItem.Value)
            //{
            //    tempList.Add(item.Clone(item));
            //}
            //this.ListOrderItem.Value = tempList;
        }
    }
}