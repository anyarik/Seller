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

namespace FoodPoint_Seller.Core.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        //TODO Необходимо:
        // Получать текущие активные заказы магазина на входе
        // Кешировать\сохранять\получать активные заказы заказы
        // Таймер для оплаченного заказа
        // Решить выносить ли логику с заказами в одтельный сервис
        // Реализовать кнопки в списке добавок полученного заказа
        // Выключать таймер когда заказ отправлен и запускать новый под новый этап.
        // Просмотр полного заказа, при нажатии на него.

        private IOrderController _orderController;
        private ISellerAuthService _loginService;
        private readonly IMvxMessenger _messenger;
  
        /// <summary>Gets the recycler.</summary>
        /// <value>The recycler.</value>
        //public RecyclerViewModel Recycler { get; private set; }

        /// <summary> 
        /// Список заказов, который получены и согласованы
        /// </summary>
        public INC<List<PayedOrder>> ListOrderItem = new NC<List<PayedOrder>>(new List<PayedOrder>(), (e) => {
        });

        public INC<bool> IsClikedOrderDialogOpen = new NC<bool>(false, (e) =>
        {
        });

        
        public INC<int> CountStackOrder = new NC<int>(0, (e) =>
       {
       });
        
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
        /// Выбраное  время переноса заказа продовцом,  отображаемое на диалоговом окне пришедшего заказа
        /// </summary>
        public INC<string> DelayTime = new NC<string>("00:00", (e) =>
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

        public INC<string> RecivedOrderTimer = new NC<string>("", (e) =>
        {
        }); 
        #endregion

        private event EventHandler<RecivedOrder> OpenNexStackOrder;

        private MvxCommand<ProductForOrder> _productAcceptedCommand;
        private MvxCommand<ProductForOrder> _additiveAcceptedCommand;

        public HomeViewModel(IOrderController orderController, IMvxMessenger messenger, ISellerAuthService loginService)
        {
            this._orderController = orderController;
            this._loginService = loginService;

            this._messenger = messenger;
        }
        /// <summary>
        /// Метод, который говорит нам о том, что наша ViewModel отобразилась на экране
        /// </summary>
        public override void Start()
        {
            base.Start();

            _messenger.Subscribe<ProductAcceptedMessage>(message =>
            {
                ProductAcceptedCommand.Execute(message.Product);
            }, MvxReference.Strong);

            _messenger.Subscribe<AddictiveAcceptedMessage>(message =>
            {
                AdditiveAcceptedCommand.Execute(message.Additive);
            }, MvxReference.Strong);

            this.OpenNexStackOrder += HomeViewModel_OpenNexStackOrder;
            this.Init();
            //this.ListOrderItem.Value = _orderService.GetOrders();
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

                this.CountStackOrder.Value--;
            }
        }
        public ICommand ProductAcceptedCommand
        {
            get
            {
                _productAcceptedCommand = _productAcceptedCommand ?? new MvxCommand<ProductForOrder>(product =>
                {
                   if (this._sendOrder.DelayProduct.Find(p=>p.ID == product.ID) == null)
                    {
                        var tempProduct = product;
                        tempProduct.PossibleAdditives = null;
                        this._sendOrder.DelayProduct.Add(tempProduct);
                    }

                    else
                    {
                        var tempProduct = product;
                        tempProduct.PossibleAdditives = null;
                        this._sendOrder.DelayProduct.Remove(tempProduct);
                    }

                    this.SetStatusOrder();
                });
                return _productAcceptedCommand;
            }
        }

        public ICommand AdditiveAcceptedCommand
        {
            get
            {
                _additiveAcceptedCommand = _additiveAcceptedCommand ?? new MvxCommand<ProductForOrder>(additive =>
                {
                    //if (this._recivedOrder.DelayProduct.Find(p => p.ID == product.ID) != null)
                    //{
                    //}

                    //else
                    //{
                    //}

                    //this.SetStatusOrder();
                });
                return _additiveAcceptedCommand;
            }
        }

        private void SetStatusOrder()
        {
            if (this._sendOrder.DelayProduct.Count > 0 && this._sendOrder.DelayTime == null)
                this._sendOrder.StatusOrder = false;
            else
                this._sendOrder.StatusOrder = true;
        }

        private  async void Init()
        {
            var user = await this._loginService.GetProfileSeller();
            //this._orders = await _orderController.GetActiveOrders();
            this._orderController.HubConnection(user.ID);

            this._orderController.OnReceiveOrder((customer, reciveOrder, time) =>
            {
                var deserializeOrder = JsonConvert.DeserializeObject<OrderItem>(reciveOrder);

                //this.ListOrderItem.Value.Add(deserializeOrder);
                //var tempList = new List<OrderItem>();

                //foreach (var item in this.ListOrderItem.Value)
                //{
                //    tempList.Add(item.Clone(item));
                //}

                //this.ListOrderItem.Value = tempList;

                var stackOrder = new RecivedOrder(customer.ToString(), time, deserializeOrder, (order) => {
                    order.CloseOrderTimer = new Timer(new TimeSpan(0, 0, 90), (_) =>
                    {
                        order.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);
                        
                        if(order.IsAlive)
                            this.RecivedOrderTimer.Value = order.CloseOrderTimer.WaitTime.ToString();

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
                    this.CountStackOrder.Value = this._recivedStackOrders.Count;

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
        
        /// <summary>
        /// Откритытие окна согласования заказа
        /// </summary>
        private void OpenDialogForOrderAgreement(RecivedOrder currentOrder)
        {
            this._sendOrder = new RecivedOrder(currentOrder.CustomerName, currentOrder.Time, currentOrder.Order, null);

            currentOrder.IsAlive = true;

            this.RecivedOrderTime.Value = currentOrder.Time;
            this.RecivedOrderNumber.Value = currentOrder.Order.RowNumber;

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
            var recivedCorrectProduct = JsonConvert.SerializeObject(this._sendOrder.DelayProduct);
            this._orderController.CorrectOrder(this._sendOrder.CustomerName, this._sendOrder.StatusOrder, recivedCorrectProduct,
                                                                                             this._sendOrder.DelayTime);

            var user = await this._loginService.GetProfileSeller();

            this._orderController.SetSellerOrder(this._sendOrder.Order.ID.ToString(), user.ID);

            var doneOrder = new PayedOrder(this._sendOrder.CustomerName, this._sendOrder.Order, TimeSpan.Parse(this._sendOrder.Time), (order) => {
                order.CloseOrderTimer = new Timer(order.OrderTime, (_) =>
                {
                    order.CloseOrderTimer.WaitTime -= new TimeSpan(0, 0, 1);
                });
            });
            doneOrder.StartTimer();

            this.ListOrderItem.Value.Add(doneOrder);
            var tempList = new List<PayedOrder>();

            foreach (var item in this.ListOrderItem.Value)
            {
                tempList.Add(item);
            }

            this.ListOrderItem.Value = tempList;

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
        public void OndelayFive()
        {
            DelayTime.Value = "05:00";
            this._sendOrder.DelayTime = "5";
            this._sendOrder.StatusOrder = false;
        }
        #endregion

        public void OrderClick(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = true;

            this.RecivedOrderNumber.Value = order.Order.ID.ToString();
            this.ListCurentOrderProductItem.Value = order.Order.OrderedFood;
        }
        public void OnClose(PayedOrder order)
        {
            this.IsClikedOrderDialogOpen.Value = false;
        }
     }
}