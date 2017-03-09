using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services.Implementations;
using Newtonsoft.Json;
//using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.Services.Implementations
{
    public class SellerOrderService:ISellerOrderService
    {
        private List<PayedOrder> _activeOrders = new List<PayedOrder>();
        
        public event EventHandler<string> ChangeStatus;

        private string _curentStatus = "";
        public string CurentStatus {
            get { return _curentStatus ?? ""; }
            set {
                _curentStatus = value;
                ChangeStatus?.Invoke(null, _curentStatus);
            }
        }

        private IOrderController _orderController;
        private ISellerAuthService _sellerAuthService;

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
        
        public SellerOrderService(IOrderController orderController, ISellerAuthService sellerAuthService)
        {
            this._orderController = orderController;
            this._sellerAuthService = sellerAuthService;

            this.Init();
            CurentStatus = "";

            this._orderController.OnChangeStatusSeller((_, status) =>
            {
                CurentStatus = status;
            });

            //_activeOrders.
        }

        private async void Init()
        {
            //if (_activeOrders.Count == 0)
            //{
            //    await InitActiveOrder();
            //}

            _orderController.OnGettingPurchasedOrders((customer, order) =>
            {
                var deserializeOrder = JsonConvert.DeserializeObject<OrderItem>(order);

                var payedOrder = new PayedOrder(deserializeOrder.WhoSold, deserializeOrder, deserializeOrder.Timer, (orderInProcess) =>
                {
                    orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, (_) =>
                    {
                        orderInProcess.CloseOrderTimer.WaitTime.Value -= new TimeSpan(0, 0, 1);
                        if (orderInProcess.CloseOrderTimer.WaitTime.Value <= TimeSpan.Zero)
                        {
                            try
                            {
                                //_dialogService.Notification(new NotificaiosModel($"Время приготовления заказа №{addOrder.Order.RowNumber} закончилось"
                                //                                                             , "")
                                //);
                                orderInProcess.StopTimer();
                                orderInProcess.IsOrderFinished.Value = true;
                            }
                            catch (Exception exp)
                            {
                                var a = exp.Message;
                            }
                        }
                    });
                });

                payedOrder.StartTimer();


                this.AddActiveOrder(payedOrder);
                //if (!_activeOrders.Any(o => o.Order.ID == payedOrder.Order.ID))
                //{
                    //_activeOrders.Add(payedOrder);
                    onNewPayedOrder.Invoke(null, payedOrder);
                //}

                
            });
        }

        private void AddActiveOrder(PayedOrder payedOrder)
        {
            var item = this._activeOrders.Count > 0
                ? this._activeOrders.FirstOrDefault((o) => o.Order.ID == payedOrder.Order.ID)
                : null;
            if (item == null)
                _activeOrders.Add(payedOrder);
        }

        public async Task<List<PayedOrder>> GetOrders()
        {
            if (_activeOrders.Count == 0)
            {
                await InitActiveOrder();
            }
            var a =  new List<PayedOrder>();
            foreach (var item in _activeOrders)
            {
                a.Add(item.Clone(item));
            }

            return a;
        }

        public void DeletOrder(OrderItem deletOrder)
        {
            _activeOrders.RemoveAll(o => o.Order.ID == deletOrder.ID);
        }

        private async Task InitActiveOrder()
        {
            var seller = await _sellerAuthService.GetProfile();
            var token = await _sellerAuthService.GetToken();

            //var recivedActiveOrdrers = await Policy.Handle<Exception>(_ => true)
            //                                    .WaitAndRetryForeverAsync
            //                                    (
            //                                        sleepDurationProvider: retry => TimeSpan.FromSeconds(10)
            //                                    )
            //                                    .ExecuteAsync(async () => await _orderController.GetActiveOrders(seller.ID, token));
            var recivedActiveOrdrers = await _orderController.GetActiveOrders(seller.ID, token);

            foreach (var item in recivedActiveOrdrers)
            {
                var activeOrder = new PayedOrder("", item, item.Timer, (orderInProcess) =>
                {
                    orderInProcess.CloseOrderTimer = new Timer(orderInProcess.OrderTime, (_) =>
                    {
                        orderInProcess.CloseOrderTimer.WaitTime.Value -= new TimeSpan(0, 0, 1);
                        if (orderInProcess.CloseOrderTimer.WaitTime.Value <= TimeSpan.Zero)
                        {
                            try
                            {
                                //_dialogService.Notification(new NotificaiosModel($"Время приготовления заказа №{addOrder.Order.RowNumber} закончилось"
                                //                                                             , "")
                                //);
                                orderInProcess.StopTimer();
                                orderInProcess.IsOrderFinished.Value = true;
                            }
                            catch (Exception exp)
                            {
                                var a = exp.Message;
                            }

                        }

                    });
                });
                activeOrder.StartTimer();

                //if (!_activeOrders.Any(o=>o.Order.ID == activeOrder.Order.ID))
                //{
                //    _activeOrders.Add(activeOrder);
                //}
                this.AddActiveOrder(activeOrder);
               
            }
        }
    }
}
