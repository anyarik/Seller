
using System;
using FoodPoint_Seller.Core.Models;
using FoodPoint_Seller.Core.Services;

namespace FoodPoint_Seller.Touch.Services
{
    public class TouchDialogService : IDialogService
    {
        public event EventHandler<NotificaiosModel> NotificateIt;

        public void Alert(string message, string title, string okbtnText)
        {
        }

        public void Notification(NotificaiosModel notification)
        {
            
        }
    }
}
