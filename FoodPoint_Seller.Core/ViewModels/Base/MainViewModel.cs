﻿using Acr.UserDialogs;
using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.ViewModels;
using FoodPoint_Seller.Core.Extentions;
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
    public enum StatusOrder
    {
        refused,
        over,
        NoTime,
        NoProduct,
        Accept
    }
	public class MainViewModel : BaseViewModel
    {
        private readonly IOrderController _orderController;

        private readonly ISellerOrderService _sellerOrderService;
        private readonly ISellerAuthService _loginService;
        private readonly IDialogService _dialogService;

        private readonly IUserDialogs _userArcDialogs;

        private event EventHandler<RecivedOrder> OpenNexStackOrder;

        public  MainViewModel( IOrderController orderController
                             , IDialogService dialogService
                             , IUserDialogs userArcDialogs
                             , ISellerAuthService loginService
                             , ISellerOrderService sellerOrderService)
        {
            this._orderController = orderController;
            this._loginService = loginService;
            this._sellerOrderService = sellerOrderService;

            this._dialogService = dialogService;
            this._userArcDialogs = userArcDialogs;

            _userArcDialogs.Loading("Загрузка").Hide();

            this.OpenNexStackOrder += HomeViewModel_OpenNexStackOrder;
            this._sellerOrderService.OnNewPayedOrder += _sellerOrderService_OnNewPayedOrder;

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
        }

        public override void Start()
        {
            //base.Start();
           
        }
        public void ShowMenu()
        {
            ShowViewModel<HomeViewModel>();
            ShowViewModel<MenuViewModel>();

            this.InitSignalRForAgreement();
        }

        public void ShowHome()
        {
            ShowViewModel<HomeViewModel>();
        }

        #region Окно согласования заказ
        #region Переменые для окна согласования заказа
        /// <summary>
        /// Информация о полученом заказе, который в обработке продовцом
        /// </summary>
        private RecivedOrder _sendOrder;

        /// <summary>
        /// Очередь заказов, которые еще не обработаны.
        /// </summary>
        private List<RecivedOrder> _recivedStackOrders = new List<RecivedOrder>();

        /// <summary>
        /// Список заказов в очереди
        /// </summary>
        public INC<List<RecivedOrder>> RecivedStackOrders = new NC<List<RecivedOrder>>(new List<RecivedOrder>(), (e) =>
        {
        });

        /// <summary>
        /// Список текущих опаченых заказов, которые готовятся
        /// </summary>
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
        public INC<TimeSpan> RecivedOrderTime = new NC<TimeSpan>();

        /// <summary>
        /// Номер заказа, отображаемое на диалоговом окне пришедшего заказа
        /// </summary>
        public INC<string> RecivedOrderNumber = new NC<string>("", (e) =>
        {
        });

        public INC<TimeSpan> RecivedOrderTimer = new NC<TimeSpan>();

        public INC<bool> IsDelayFive;
        #endregion

        #region Функции для согласования заказа
        /// <summary>
        /// Событие для  открытия следующего заказа 
        /// </summary>
        private void HomeViewModel_OpenNexStackOrder(object sender, RecivedOrder deleteOrder)
        {
            this.ClearOrder(deleteOrder);
            //IsDelayFive.Value = false;
            if (!this._recivedStackOrders.IsNullOrEmpty())
            {
                var curerntOrder = this._recivedStackOrders.FirstOrDefault();
                this.OpenDialogForOrderAgreement(curerntOrder);
            }
        }
        
        private async void InitSignalRForAgreement()
        {
            var user = await this._loginService.GetProfile();
            if (user != null)
            {
                this._orderController.HubConnection(user.ID);
                this._orderController.OnReceiveOrder(async (customer, reciveOrder, time) =>
               {
                   var deserializeOrder = JsonConvert.DeserializeObject<OrderItem>(reciveOrder);

                   _dialogService.Notification(new NotificaiosModel($"Пришел на обработку заказ №{deserializeOrder.RowNumber}"
                                                   , $"Необходимо приготовить его за {TimeSpan.Parse(time).Minutes} минут")
                              );

                   var stackOrder = new RecivedOrder(customer.ToString(), time, deserializeOrder, (order) =>
                   {
                       order.CloseOrderTimer = new Timer(new TimeSpan(0, 0, 75), (_) =>
                       {
                           order.CloseOrderTimer.WaitTime.Value -= new TimeSpan(0, 0, 1);

                          // UpdateStackOrderList();

                           if (order.IsAlive)
                               this.RecivedOrderTimer.Value = order.CloseOrderTimer.WaitTime.Value;

                           if (order.CloseOrderTimer.WaitTime.Value >= TimeSpan.Zero) return;

                           order.CloseOrderTimer.StopTimer();
                           this._recivedStackOrders.RemoveAll(o => o.Order.ID == order.Order.ID);

                           if (this.IsOrderDialogOpen.Value)
                               this.OpenNexStackOrder.Invoke(null, null);
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

                   this.CurentPayedOrders.Value = await _sellerOrderService.GetOrders();
               });
            }
        }

        private void _sellerOrderService_OnNewPayedOrder(object sender, PayedOrder e)
        {
            this.UpdatePayedOrderList(e);
        }
        private void UpdateStackOrderList()
        {
            var tempList = this.RecivedStackOrders.Value.Select(item => item.Clone(item)).ToList();

            this.RecivedStackOrders.Value = tempList;
        }

        private void UpdatePayedOrderList(PayedOrder addingOrder)
        {
            this.CurentPayedOrders.Value.Add(addingOrder);
            var tempList = this.CurentPayedOrders.Value.Select(item => item.Clone(item)).ToList();
            this.CurentPayedOrders.Value = tempList;
        }

        /// <summary>
        /// Откритытие окна согласования заказа
        /// </summary>
        private void OpenDialogForOrderAgreement(RecivedOrder currentOrder)
        {
            this._sendOrder = new RecivedOrder(currentOrder.CustomerName, currentOrder.Time, currentOrder.Order, null);

            if (this.RecivedStackOrders.Value.FirstOrDefault(o => o.Order.ID == _sendOrder.Order.ID) != null)
            {
                this.RecivedStackOrders.Value.RemoveAll(o => o.Order.ID == _sendOrder.Order.ID);

                UpdateStackOrderList();
            }

            currentOrder.IsAlive = true;

            this.RecivedOrderTime.Value = TimeSpan.Parse(currentOrder.Time);
            this.RecivedOrderNumber.Value = currentOrder.Order.RowNumber;

            currentOrder.Order.OrderedFood = currentOrder.Order.OrderedFood.OrderBy(f => f.ProductInfo.Name).ToList();

            var indexFood = 0;
            foreach (var item in currentOrder.Order.OrderedFood)
            {
                indexFood++;
                item.index = indexFood;
            }

            //indexFood = 0;

            this.ListCurentOrderProductItem.Value = currentOrder.Order.OrderedFood;

            IsOrderDialogOpen.Value = true;
        }

        #region Обработка нажатия кнопок
        /// <summary>
        ///отменить заказ
        /// </summary>
        public async void OnCancelOrder()
        {
            _userArcDialogs.Loading("Загрузка");
            var recivedCorrectOrder = JsonConvert.SerializeObject(this._sendOrder.Order);

            //1
            this._orderController.CorrectOrder(this._sendOrder.CustomerName, false, recivedCorrectOrder, "false");

            var user = await this._loginService.GetProfile();

            //2
            this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString()
                                                    , StatusOrder.refused.ToString(), false, TimeSpan.Zero, "", false);

            this.OpenNexStackOrder.Invoke(null, this._sendOrder);
            _userArcDialogs.Loading("Загрузка").Hide();
        }

        /// <summary>
        /// Согласовать заказ
        /// </summary>
        public  void OnApprove()
        {
            _userArcDialogs.Loading("Загрузка").Show();
            try
            {
                var recivedCorrectOrder = JsonConvert.SerializeObject(this._sendOrder.Order);

                //TODO Сделать загрузку при согласовании и внедрить поли
                //1
                this._orderController.CorrectOrder(this._sendOrder.CustomerName
                                                  , true, recivedCorrectOrder, this.IsDelayFive.Value.ToString());

                var isDeleteProduct = false;

                foreach (var product in this._sendOrder.Order.OrderedFood)
                    if (!product.ProductInfo.IsActive)
                        isDeleteProduct = true;
                    else
                        foreach (var additive in product.ProductInfo.OrderedAdditives)
                            if (!additive.IsActive)
                                isDeleteProduct = true;

                //2 || 3
                if (this.IsDelayFive.Value && isDeleteProduct)
                {
                    this._orderController.ChangeStatusOrder( this._sendOrder.Order.ID.ToString()
                                                           , StatusOrder.NoTime.ToString()
                                                           , true
                                                           , TimeSpan.FromMinutes(5)
                                                           , StatusOrder.over.ToString()
                                                           , true);
                    this._orderController.SaveOfferedFood(this._sendOrder.Order.ID.ToString()
                                                         , this._sendOrder.Order.OrderedFood, null);
                }
                else if (this.IsDelayFive.Value)
                    this._orderController.ChangeStatusOrder( this._sendOrder.Order.ID.ToString()
                                                           , StatusOrder.NoTime.ToString()
                                                           , true
                                                           , TimeSpan.FromMinutes(5)
                                                           , StatusOrder.over.ToString()
                                                           , true);

                else if (isDeleteProduct)
                {
                    this._orderController.ChangeStatusOrder( this._sendOrder.Order.ID.ToString()
                                                           , StatusOrder.NoProduct.ToString()
                                                           , true
                                                           , TimeSpan.FromMinutes(5)
                                                           , StatusOrder.over.ToString()
                                                           , true);
                    this._orderController.SaveOfferedFood(this._sendOrder.Order.ID.ToString()
                                                         , this._sendOrder.Order.OrderedFood, null);
                }

                else
                    this._orderController.ChangeStatusOrder( this._sendOrder.Order.ID.ToString()
                                                           , StatusOrder.Accept.ToString()
                                                           , true
                                                           , TimeSpan.FromMinutes(5)
                                                           , StatusOrder.over.ToString()
                                                           , true);

                this.OpenNexStackOrder.Invoke(null, this._sendOrder);
            }
            catch (Exception)
            {
                _userArcDialogs.Alert("Ошибка в отправке ответа", "Подключитесь к интернету");
            }
            _userArcDialogs.Loading().Hide();
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
        #endregion
        #endregion
    }
}