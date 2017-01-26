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

        public  MainViewModel(IOrderController orderController, IDialogService dialogService, ISellerAuthService loginService)
        {
            this._orderController = orderController;
            this._loginService = loginService;

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

                _dialogService.Notification(new NotificaiosModel(deserializeOrder.RowNumber, deserializeOrder.orderTimer));

                var stackOrder = new RecivedOrder(customer.ToString(), time, deserializeOrder, (order) =>
                {
                    order.CloseOrderTimer = new Timer(new TimeSpan(0, 0, 75), (_) =>
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

            this._orderController.OnCustomerAgreed((_, state) =>
            {
                //if (state)new 
                //      this.textOrder.Text += $"\nПодтвердил";
                //  else
                //      this.textOrder.Text += $"\nОтменил";
            });
        }

        private void UpdateStackOrderList()
        {
            var tempList = new List<RecivedOrder>();
            foreach (var item in this.RecivedStackOrders.Value)
            {
                tempList.Add(item.Clone(item));
            }
            this.RecivedStackOrders.Value = tempList;
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
            this._orderController.CorrectOrder(this._sendOrder.CustomerName, false, "[]", null);

            var user = await this._loginService.GetProfileSeller();

            this._orderController.SetSellerOrder(this._sendOrder.Order.ID.ToString(), user.ID);
            this._orderController.ChangeStatusOrder(this._sendOrder.Order.ID.ToString(), "refused", false);

            this.OpenNexStackOrder.Invoke(null, this._sendOrder);
        }

        /// <summary>
        /// Согласовать заказ
        /// </summary>
        public async void OnApprove()
        {
            var recivedCorrectOrder = JsonConvert.SerializeObject(this._sendOrder.Order);
            this._orderController.CorrectOrder(this._sendOrder.CustomerName, true, recivedCorrectOrder, IsDelayFive.Value.ToString());

            var user = await this._loginService.GetProfileSeller();

            this._orderController.SetSellerOrder(this._sendOrder.Order.ID.ToString(), user.ID);

            var doneOrder = new PayedOrder(this._sendOrder.CustomerName, this._sendOrder.Order, TimeSpan.Parse(this._sendOrder.Time), (order) =>
            {
                order.CloseOrderTimer = new Timer(order.OrderTime, (_) =>
                {
                    order.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);
                });
            });
            doneOrder.StartTimer();

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