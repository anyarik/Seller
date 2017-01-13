using FoodPoint_Seller.Api.Models.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services.Implementations
{
   public class OrderHubService: IOrderHubService
    {
        private HubConnection connection;
        private IHubProxy _hub;
        private string userId;
        private bool isConnected;

        private event EventHandler<IDictionary<string,string>> receiveOrder;
        private event EventHandler<bool> customerAgreedYAY;

        private event EventHandler<string> setStatusSeller;

        event EventHandler<IDictionary<string, string>> IOrderHubService.receiveOrder
        {
            add
            {
                this.receiveOrder += value;
            }

            remove
            {
                this.receiveOrder -= value;
            }
        }

        event EventHandler<bool> IOrderHubService.customerAgreedYAY
        {
            add
            {
                this.customerAgreedYAY += value;
            }

            remove
            {
                this.customerAgreedYAY -= value;
            }
        }

        event EventHandler<string> IOrderHubService.setStatusSeller
        {
            add
            {
                this.setStatusSeller += value;
            }

            remove
            {
                this.setStatusSeller -= value;
            }
        }

        public async void HubConnection(string id)
        {
            this.userId = id;
            if (this._hub == null)
            {
                 connection = new HubConnection(AppData.Host, new Dictionary<string, string>() { { "UserName", id}, { "IsSeller", "True" } });
                _hub = connection.CreateHubProxy("OrderHandlerHub");
            }

            this.InitConectinEvents();

            this.IntHubSubscriptions();
        }

        private void IntHubSubscriptions()
        {
             _hub.On("OnСonnected", () => {

            });

            _hub.On("OnReconnected", () => {

            });

            _hub.On("OnDisconnected", () => {

            });

            _hub.On<string, string, string>("RecieveOrder", (customer, order, time) =>
            {
                var orderDictionry = new Dictionary<string, string>()
                {
                     {"order", order},
                     {"time", time},
                };
                //var customerOrder = JsonConvert.DeserializeObject<Order>(order);
                //var orderResult = $"Заказ:{JsonConvert.SerializeObject(customerOrder.OrderedFood)}.\nОт:{customer}\nПриготовить через:{time}";
                this.receiveOrder.Invoke(customer, orderDictionry);
            });

            _hub.On<string, bool>("CustomerAgreedYAY", (customer, agreed) =>
            {
                this.customerAgreedYAY.Invoke(null, agreed);
            });
        }

        private void InitConectinEvents()
        {
            connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    setStatusSeller.Invoke(null, "offline");
                }
                else
                {
                    setStatusSeller.Invoke(null, "online");
                }
            });



            connection.Closed += Connection_Closed;

            connection.ConnectionSlow += Connection_ConnectionSlow;

            connection.Error += Connection_Error;

            connection.Reconnected += Connection_Reconnected;

            connection.Reconnecting += Connection_Reconnecting;

            connection.Received += Connection_Received;
        }

        private void Connection_Received(string obj)
        {
            
        }

        private async void Connection_Reconnecting()
        {
            //await connection.Start();
            //connection.Stop();

            setStatusSeller.Invoke(null, "conecting");
        }

        private async void Connection_Reconnected()
        {
           var state =  connection.State;
            setStatusSeller.Invoke(null, "conected");

            //isConnected = false;
            //while (!isConnected)
            //{
            //    connection = new HubConnection(AppData.Host, new Dictionary<string, string>() { { "UserName", userId }, { "IsSeller", "True" } });
            //    _hub = connection.CreateHubProxy("OrderHandlerHub");
            //    await connection.Start().ContinueWith(task =>
            //    {
            //        if (task.IsFaulted)
            //        {
            //            isConnected = false;
            //            setStatusSeller.Invoke(null, isConnected);
            //        }
            //        else
            //        {
            //            isConnected = true;
            //            setStatusSeller.Invoke(null, isConnected);
            //        }
            //    });
            //    //connection.Stop();
            //}
        }

        private async void Connection_Error(Exception obj)
        {
            //await connection.Start();
            //connection.Stop();
        }

        private async void Connection_ConnectionSlow()
        {
            //await connection.Start();
            setStatusSeller.Invoke(null, "internet problem");
            //connection.Stop();
        }

        private async void Connection_Closed()
        {
            setStatusSeller.Invoke(null, "offline");

            //await  connection.Start().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        isConnected = false;
            //        setStatusSeller.Invoke(null, isConnected);
            //    }
            //    else
            //    {
            //        isConnected = true;
            //        setStatusSeller.Invoke(null, isConnected);
            //    }
            //});
            //connection.Stop();

            //while (!isConnected)
            //{
            //connection = new HubConnection(AppData.Host, new Dictionary<string, string>() { { "UserName", userId }, { "IsSeller", "True" } });
            //_hub = connection.CreateHubProxy("OrderHandlerHub");
            //connection.Start().ContinueWith(task =>
            //{
            //    if (task.IsFaulted)
            //    {
            //        isConnected = false;
            //        setStatusSeller.Invoke(null, isConnected);
            //    }
            //    else
            //    {
            //        isConnected = true;
            //        setStatusSeller.Invoke(null, isConnected);
            //    }
            //});
            //}
        }

        public void HubDisconnect()
        {
            isConnected = false;
            setStatusSeller.Invoke(null, "offline");
            connection.Stop();
        }
        public void CorrectOrder(string customerId, bool status, string corectOrder, string corectTime )
        {
            _hub.Invoke("CorrectOrder", customerId, status.ToString(), corectOrder, corectTime);

        }
 
    }
}
