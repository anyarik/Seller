using FoodPoint_Seller.Api.Models.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNet.SignalR.Client.Hubs;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Client.Transports;
using System.Reactive.Linq;
using FoodPoint_Seller.Api.Utils;
using Polly;

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
        private event EventHandler<string> gettingPurchasedOrders;

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
        event EventHandler<string> IOrderHubService.gettingPurchasedOrders
        {
            add
            {
                this.gettingPurchasedOrders += value;
            }

            remove
            {
                this.gettingPurchasedOrders -= value;
            }
        }

        public  void HubConnection(string id)
        {
            this.userId = id;

            connection = new HubConnection(AppData.Host, new Dictionary<string, string>() { { "UserName", id }, { "IsSeller", "True" }, { "Action", "Отключился сам" } });
            _hub = connection.CreateHubProxy("OrderHandlerHub");

            this.InitConectinEvents();
            this.IntHubSubscriptions();
        }

        private  void IntHubSubscriptions()
        {

            _hub.On<string, string, string>("RecieveOrder", (customer, order, time) =>
            {
                var orderDictionry = new Dictionary<string, string>()
                {
                     {"order", order},
                     {"time", time},
                };

                this.receiveOrder.Invoke(customer, orderDictionry);
            });

            _hub.On<string, bool>("CustomerAgreedYAY", (customer, agreed) =>
            {
                this.customerAgreedYAY.Invoke(null, agreed);
            });

            _hub.On<string>("NotificateAboutOrder", (order) =>
            {
                this.gettingPurchasedOrders.Invoke(null, order);
            });
        }

        private  void InitConectinEvents()
        {
            connection.Start(new LongPollingTransport()).ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    setStatusSeller.Invoke(null, "offline");
                }
                else
                {
                    this.isConnected = true;
                    setStatusSeller.Invoke(null, "online");
                }
            });

            connection.Closed += Connection_Closed;

            connection.ConnectionSlow += Connection_ConnectionSlow;

            connection.Error += Connection_Error;

            connection.Reconnected += Connection_Reconnected;

            connection.Reconnecting += Connection_Reconnecting;

            connection.Received += Connection_Received;

            connection.StateChanged += Connection_StateChanged;
        }

        private void Connection_StateChanged(StateChange st)
        {
            if (st.NewState == ConnectionState.Disconnected)
            {
                setStatusSeller.Invoke(null, "offline");
                connection.Stop();
           }

            if (!isConnected && st.OldState == ConnectionState.Connecting && st.NewState == ConnectionState.Connected)
            {
                setStatusSeller.Invoke(null, "online");
            }
        }

        private void Connection_Received(string obj)
        {
            
        }

        private  void Connection_Reconnecting()
        {
            setStatusSeller.Invoke(null, "reconecting...");
        }

        private void Connection_Reconnected()
        {
           var state =  connection.State;
           setStatusSeller.Invoke(null, "online");

        }

        private  void Connection_Error(Exception obj)
        {
           
        }

        private  void Connection_ConnectionSlow()
        {
            setStatusSeller.Invoke(null, "internet problem");
        }

        private async void Connection_Closed()
        {
            if (isConnected)
            {
                setStatusSeller.Invoke(null, "conecting...");
                await Policy.Handle<Exception>(_ => true)
                                            .WaitAndRetryForeverAsync
                                            (
                                                sleepDurationProvider: retry => TimeSpan.FromSeconds(10)
                                            )
                                            .ExecuteAsync(async () => await Connect());
            }

        }

        public async Task<bool> Connect()
        {
          
            return await connection.Start().ContinueWith(a=>
            {

                if (a.IsFaulted)
                {
                    throw new Exception();
                }
                else
                {               
                    return true;
                }
            });
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
