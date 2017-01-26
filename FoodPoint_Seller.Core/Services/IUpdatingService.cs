using FoodPoint_Seller.Core.Models;
using System;

namespace FoodPoint_Seller.Core.Services
{
    public interface IUpdatingService
    {
        void Update();

        event EventHandler<NotificaiosModel> NotificateIt;

    }
}