using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services;
using MvvmCross.FieldBinding;
using MvvmCross.Plugins.Messenger;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace FoodPoint_Seller.Core.ViewModels
{
	public class MainViewModel : BaseViewModel
    {
        private IOrderController _orderController;

        private ISellerOrderService _sellerOrderService;
        private ISellerAuthService _loginService;

        private readonly IDialogService _dialogService;
        private int indexFood = 0;
       
        //public INC<string> TextActiveSeller = new NC<string>("offline", (e) =>
        //{
        //});
      

        #region Переменые для окна согласования заказа
        /// <summary>
        /// Информация о полученом заказе, который в обработке продовцом
        /// </summary>
        private RecivedOrder _sendOrder;


        /// <summary>
        /// Очередь заказов, которые еще не обработаны.
        /// </summary>
        private List<RecivedOrder> _recivedStackOrders = new List<RecivedOrder>();

        public INC<List<RecivedOrder>> RecivedStackOrders = new NC<List<RecivedOrder>>(new List<RecivedOrder>(), (e) =>
        {
        });

        public INC<List<PayedOrder>> CurentPayedOrders = new NC<List<PayedOrder>>(new List<PayedOrder>(), (e) =>
        {
        });

        /// <summary>
        /// Список полученых текущих продуктов в пришедшем заказе
        /// </summary>
        public INC<List<ProductForOrder>> ListCurentOrderProductItem = new NC<List<ProductForOrder>>(new List<ProductForOrder>(), (e) =>
        {
        });

        /// <summary>
        /// Переменная для открытия окна пришедшего заказа
        /// </summary>
        public INC<bool> IsOrderDialogOpen = new NC<bool>(false, (e) =>
        {
        });

        /// <summary>
        /// Выбраное время для приготовления заказа покупателем, отображаемое на диалоговом окне пришедшего заказа
        /// </summary>
        public INC<string> RecivedOrderTime = new NC<string>("", (e) =>
        {
        });

        /// <summary>
        /// Номер заказа, отображаемое на диалоговом окне пришедшего заказа
        /// </summary>
        public INC<string> RecivedOrderNumber = new NC<string>("", (e) =>
        {
        });

        public INC<TimeSpan> RecivedOrderTimer = new NC<TimeSpan>(TimeSpan.Zero, (e) =>
        {
        });

        public INC<bool> IsDelayFive; 
        #endregion

        private event EventHandler<RecivedOrder> OpenNexStackOrder;

        public  MainViewModel(IOrderController orderController, IDialogService dialogService, ISellerAuthService loginService, ISellerOrderService sellerOrderService)
        {
            this._orderController = orderController;
            this._loginService = loginService;
            this._sellerOrderService = sellerOrderService;

            this._dialogService = dialogService;

            this.OpenNexStackOrder += HomeViewModel_OpenNexStackOrder;
            IsDelayFive = new NC<bool>(false, (e) =>
            {
                if (e)
                {
                    var timeOrder = TimeSpan.Parse(this._sendOrder.Time);
                    this._sendOrder.Time = timeOrder.Add(TimeSpan.FromMinutes(5)).ToString();
                }
                else
                {
                    var timeOrder = TimeSpan.Parse(this._sendOrder.Time);
                    this._sendOrder.Time = timeOrder.Add(TimeSpan.FromMinutes(-5)).ToString();
                }
            });

            _sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;
        }

        private void _sellerOrderService_OnNewPayedOrder(object sender, PayedOrder e)
        {
            this.UpdatePayedOrderList(e);
        }

        public void ShowMenu()
        {
            try
            {
                ShowViewModel<HomeViewModel>();
            }
            catch (System.Exception a)
            {
                throw;
            }
            ShowViewModel<MenuViewModel>();
            this.Init();
        }

        public void ShowHome()
        {
            ShowViewModel<HomeViewModel>();
        }

        /// <summary>
        /// Событие для  открытия следующего заказа 
        /// </summary>
        private void HomeViewModel_OpenNexStackOrder(object sender, RecivedOrder deleteOrder)
        {
            this.ClearOrder(deleteOrder);
            //IsDelayFive.Value = false;
            if (this._recivedStackOrders.Count > 0)
            {
                var curerntOrder = this._recivedStackOrders.FirstOrDefault();
                this.OpenDialogForOrderAgreement(curerntOrder);
            }
        }

        private async void Init()
        {
           var user = await this._loginService.GetProfileSeller();

            this._orderController.HubConnection(user.ID);

            this._orderController.OnReceiveOrder((customer, reciveOrder, time) =>
            {
                var deserializeOrder = JsonConvert.DeserializeObject<OrderItem>(reciveOrder);

                _dialogService.Notification(new NotificaiosModel(deserializeOrder.RowNumber, deserializeOrder.Timer));

                var stackOrder = new RecivedOrder(customer.ToString(), time, deserializeOrder, (order) =>
                {
                    order.CloseOrderTimer = new Timer(new TimeSpan(0, 5, 0), (_) =>
                    {
                        order.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);

                        UpdateStackOrderList();

                        if (order.IsAlive)
                            this.RecivedOrderTimer.Value = order.CloseOrderTimer.WaitTime;

                        if (order.CloseOrderTimer.WaitTime == TimeSpan.Zero)
                        {
                            order.CloseOrderTimer.StopTimer();

                            this._recivedStackOrders.RemoveAll(o => o.Order.ID == order.Order.ID);

                            if (this.IsOrderDialogOpen.Value)
                                this.OpenNexStackOrder.Invoke(null, null);
                        }
                    });
                });

                stackOrder.StartTimer();
                
                this._recivedStackOrders.Add(stackOrder);

                if (this._recivedStackOrders.Count > 1)
                {
                    this.RecivedStackOrders.Value.Add(stackOrder);
                    UpdateStackOrderList();
                }
      
                if (this._recivedStackOrders.Count == 1)
                    this.OpenDialogForOrderAgreement(stackOrder);
            });

            this.CurentPayedOrders.Value = _sellerOrderService.GetOrders();
            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder1; 
        }

        private void _sellerOrderService_OnNewPayedOrder1(object sender, PayedOrder e)
        {
            this.UpdatePayedOrderList(e);
        }

        private void UpdateStackOrderList()
        {
            var tempList = new List<RecivedOrder>();
            foreach (var item in this.RecivedStackOrders.Value)
                tempList.Add(item.Clone(item));

            this.RecivedStackOrders.Value = tempList;
        }

        private void UpdatePayedOrderList(PayedOrder addingOrder)
        {
            this.CurentPayedOrders.Value.Add(addingOrder);
            var tempList = new List<PayedOrder>();
            foreach (var item in this.CurentPayedOrders.Value)
            {
                tempList.Add(item.Clone(item));
            }
            this.CurentPayedOrders.Value = tempList;
        }

        /// <summary>
        /// Откритытие окна согласования заказа
        /// </summary>
        private void OpenDialogForOrderAgreement(RecivedOrder currentOrder)
        {
            this._sendOrder = new RecivedOrder(currentOrder.CustomerName, currentOrder.Time, currentOrder.Order, null);

            if (this.RecivedStackOrders.Value.Where(o => o.Order.ID == _sendOrder.Order.ID).FirstOrDefault() != null)
            {
                this.RecivedStackOrders.Value.RemoveAll(o => o.Order.ID == _sendOrder.Order.ID);

                UpdateStackOrderList();
            }

            currentOrder.IsAlive = true;

            this.RecivedOrderTime.Value = currentOrder.Time;
            this.RecivedOrderNumber.Value = currentOrder.Order.RowNumber;

            foreach (var item in currentOrder.Order.OrderedFood)
            {
                indexFood++;
                item.index = indexFood;
            }

            indexFood = 0;

            this.ListCurentOrderProductItem.Value = currentOrder.Order.OrderedFood;

            IsOrderDialogOpen.Value = true;
        }

        #region Обработка нажатия кнопок
        /// <summary>
        ///отменить заказ
        /// </summary>
        public async void OnCancelOrder()
        {
            var recivedCorrectOrder = JsonConvert.SerializeObject(this._sendOrder.Order);
            this._orderController.CorrectOrder(this._sendOrder.CustomerName, false, recivedCorrectOrder, "false");

            var user = await this._loginService.GetProfileSeller();

            this._orderController.SetSellerOrder(this._sendOrder.Order.ID.ToString(), user.ID);
            this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString(), "refused", false, TimeSpan.Zero,"",false);

            this.OpenNexStackOrder.Invoke(null, this._sendOrder);
        }

        /// <summary>
        /// Согласовать заказ
        /// </summary>
        public async void OnApprove()
        {
            var recivedCorrectOrder = JsonConvert.SerializeObject(this._sendOrder.Order);
            this._orderController.CorrectOrder(this._sendOrder.CustomerName, true, recivedCorrectOrder, this.IsDelayFive.Value.ToString());

            var isDeleteProduct = false;

            foreach (var product in this._sendOrder.Order.OrderedFood)
              if (!product.ProductInfo.IsActive)
                    isDeleteProduct = true;
                else
                    foreach (var additive in product.ProductInfo.OrderedAdditives)
                        if (!additive.IsActive)
                            isDeleteProduct = true;
            

            if (this.IsDelayFive.Value && isDeleteProduct)
            {
                this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString(), 
                                                                "NoTime", true, TimeSpan.FromMinutes(5), "over", true);
                this._orderController.SaveOfferedFood(this._sendOrder.Order.ID.ToString(), this._sendOrder.Order.OrderedFood, null);
            }
            else if (this.IsDelayFive.Value)
                this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString(), 
                                                                    "NoTime", true, TimeSpan.FromMinutes(5), "over", true);

            else if (isDeleteProduct)
            {
                this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString(),
                                                                    "NoProduct", true, TimeSpan.FromMinutes(5), "over", true);
                this._orderController.SaveOfferedFood(this._sendOrder.Order.ID.ToString(), this._sendOrder.Order.OrderedFood, null);
            }
                
            else
                this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString(), 
                                                                        "Accept", true, TimeSpan.FromMinutes(5), "over", true);
            
             this.OpenNexStackOrder.Invoke(null, this._sendOrder);
        }

        /// <summary>
        /// Очистка обработаного заказа после отправке пользовтелю
        /// </summary>
        private void ClearOrder(RecivedOrder deleteOrder)
        {
            if (deleteOrder != null)
            {
                this._recivedStackOrders.Find(o => o.Order.ID == deleteOrder.Order.ID).CloseOrderTimer.StopTimer();

                this._recivedStackOrders.RemoveAll(o => o.Order.ID == deleteOrder.Order.ID);
            }

            this.ListCurentOrderProductItem.Value = new List<ProductForOrder>();
            IsOrderDialogOpen.Value = false;
            this._sendOrder = new RecivedOrder();
        }
        #endregion

        public override async void Start()
        {
            //base.Start();
           
        }
    }
}