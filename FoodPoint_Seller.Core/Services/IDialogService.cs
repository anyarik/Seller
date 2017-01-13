using FoodPoint_Seller.Core.Models;
using System;

namespace FoodPoint_Seller.Core.Services
{
    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);
        void Notification(NotificaiosModel notification);

        event EventHandler<NotificaiosModel> NotificateIt;

    }
}