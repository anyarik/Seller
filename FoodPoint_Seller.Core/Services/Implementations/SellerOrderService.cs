﻿using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Extentions;
using FoodPoint_Seller.Core.Models;
using Newtonsoft.Json;
using Polly;
//using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services.Implementations
{
    public class SellerOrderService:ISellerOrderService
    {

        private readonly IOrderController _orderController;
        private readonly ISellerAuthService _sellerAuthService;
        private readonly IDialogService _dialogService;
        
        public event EventHandler<string> ChangeStatus;
        public event EventHandler<string> ChangeExitText;
        public event EventHandler<PayedOrder> onNewPayedOrder;

        event EventHandler<PayedOrder> ISellerOrderService.OnNewPayedOrder
        {
            add
            {
                this.onNewPayedOrder += value;
            }
            remove
            {
                this.onNewPayedOrder -= value;
            }
        }

        private string _curentStatus = "";
        public string CurentStatus {
            get { return _curentStatus ?? ""; }
            set {
                _curentStatus = value;
                ChangeStatus?.Invoke(null, _curentStatus);
            }
        }
        private List<PayedOrder> _activeOrders = new List<PayedOrder>();

        public SellerOrderService(IOrderController orderController
                                 , ISellerAuthService sellerAuthService
                                 , IDialogService dialogService)
        {
            this._orderController = orderController;
            this._sellerAuthService = sellerAuthService;
            this._dialogService = dialogService;

            this.Init();
        }

        private  void Init()
        {
            CurentStatus = "";

            _orderController.OnGettingPurchasedOrders((customer, order) =>
            {
                var deserializeOrder = JsonConvert.DeserializeObject<OrderItem>(order);

                var payedOrder = new PayedOrder(deserializeOrder.WhoSold, deserializeOrder, deserializeOrder.Timer, 
                    (orderInProcess) =>
                {
                    orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, 
                        (_) =>
                    {
                        orderInProcess.CloseOrderTimer.WaitTime.Value -= new TimeSpan(0, 0, 1);
                        if (orderInProcess.CloseOrderTimer.WaitTime.Value > TimeSpan.Zero) return;
                        try
                        {
                            _dialogService.Notification(new NotificaiosModel($"Время приготовления заказа №{orderInProcess.Order.RowNumber} закончилось"
                                , "")
                            );
                            orderInProcess.CloseOrderTimer.WaitTime.Value = TimeSpan.Zero;
                            orderInProcess.StopTimer();
                            orderInProcess.IsOrderFinished.Value = true;
                        }
                        catch (Exception exp)
                        {
                            var a = exp.Message;
                        }
                    });
                });
                payedOrder.StartTimer();

                payedOrder.Order.OrderedFood = payedOrder.Order.OrderedFood.OrderBy(f => f.ProductInfo.Name).ToList();

                this.AddActiveOrder(payedOrder);

                onNewPayedOrder?.Invoke(null, payedOrder);
            });

            this._orderController.OnChangeStatusSeller((_, status) =>
            {
                CurentStatus = status;
            });
        }

        private void AddActiveOrder(PayedOrder payedOrder)
        {
            var item = this._activeOrders.Count > 0
                ? this._activeOrders.FirstOrDefault((o) => o.Order.ID == payedOrder.Order.ID)
                : null;
            if (item != null) return;

            _activeOrders.Add(payedOrder);
            ChangeExitText?.Invoke(null, "Остановить прием заказов");
        }

        public async Task<List<PayedOrder>> GetOrders()
        {
            if (_activeOrders.IsNullOrEmpty())
            {
                await InitActiveOrder();
            }

            return _activeOrders.Select(item => item.Clone(item)).ToList();
        }

        public void DeletOrder(OrderItem deletOrder)
        {
            _activeOrders.RemoveAll(o => o.Order.ID == deletOrder.ID);

            if (_activeOrders.IsNullOrEmpty())
                ChangeExitText?.Invoke(null, "Выйти");
        }

        private async Task InitActiveOrder()
        {
            var seller = await _sellerAuthService.GetProfile();
            var token = await _sellerAuthService.GetToken();

            var recivedActiveOrdrers = await Policy.Handle<Exception>(_ => true)
                                                .WaitAndRetryForeverAsync
                                                (
                                                    sleepDurationProvider: retry => TimeSpan.FromSeconds(10)
                                                )
                                                .ExecuteAsync(async () => await _orderController.GetActiveOrders(seller.ID, token));

            foreach (var item in recivedActiveOrdrers)
            {
                var activeOrder = new PayedOrder("", item, item.Timer, 
                    (orderInProcess) =>
                {
                    orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, 
                        (_) =>
                    {
                        orderInProcess.CloseOrderTimer.WaitTime.Value -= new TimeSpan(0, 0, 1);

                        if (orderInProcess.CloseOrderTimer.WaitTime.Value > TimeSpan.Zero) return;

                        try
                        {
                            _dialogService.Notification(
                                new NotificaiosModel($"Время приготовления заказа №{orderInProcess.Order.RowNumber} закончилось"
                                    , "")
                            );
                            orderInProcess.CloseOrderTimer.WaitTime.Value = TimeSpan.Zero;
                            orderInProcess.StopTimer();

                            orderInProcess.IsOrderFinished.Value = true;
                        }
                        catch (Exception exp)
                        {
                            var a = exp.Message;
                        }
                    });
                });
                activeOrder.StartTimer();
                activeOrder.Order.OrderedFood.OrderBy(f=>f.ProductInfo.Name).ToList();

                this.AddActiveOrder(activeOrder);
            }
        }

        public void DisconectSignalR()
        {
            this._orderController.HubDisconnect();
        }
    }
}
