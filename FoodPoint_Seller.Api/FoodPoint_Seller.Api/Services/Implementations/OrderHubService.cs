using FoodPoint_Seller.Api.Models.ViewModels;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Api.Services.Implementations
{
   public class OrderHubService: IOrderHubService
    {
        private HubConnection connection;
        private IHubProxy _hub;


        private event EventHandler<IDictionary<string,string>> receiveOrder;
        private event EventHandler<bool> customerAgreedYAY;

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

        public void HubConnection(string id)
        {

            if (this._hub == null)
            {
                string url = AppData.Host;
                 connection = new HubConnection(url, new Dictionary<string, string>() { { "UserName", id}, { "IsSeller", "True" } });
                _hub = connection.CreateHubProxy("OrderHandlerHub");
            }

            connection.Start().Wait();

            connection.Closed += Connection_Closed;

            connection.ConnectionSlow += Connection_ConnectionSlow;

            connection.Error += Connection_Error;

            connection.Reconnected += Connection_Reconnected;

            connection.Reconnecting += Connection_Reconnecting;

            _hub.On("OnСonnected", () => {

            });

            _hub.On("OnReconnected", () => {

            });

            _hub.On("OnDisconnected", ()=> {

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

        private void Connection_Reconnecting()
        {
            connection.Start().Wait();
            
        }

        private void Connection_Reconnected()
        {
            connection.Start().Wait();
        }

        private void Connection_Error(Exception obj)
        {
             connection.Start().ContinueWith(task =>
            {
                if (task.IsFaulted)
                {
                    
                }
                else
                {
         
                }
            });
        }

        private void Connection_ConnectionSlow()
        {
            connection.Start().Wait();
        }

        private void Connection_Closed()
        {
            connection.Start().Wait();
        }

        public void HubDisconnect()
        {
            connection.Stop();
        }
        public void CorrectOrder(string customerId, bool status, string corectOrder, string corectTime )
        {
            _hub.Invoke("CorrectOrder", customerId, status.ToString(), corectOrder, corectTime);

        }
 
    }
}
