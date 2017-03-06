using MvvmCross.Plugins.Messenger;

namespace FoodPoint_Seller.Core.Models
{
    public class ClickOnFinishOrderMessage : MvxMessage
    {
        public ClickOnFinishOrderMessage(PayedOrder payedOrder)
            :base(payedOrder)
        {
        }
    }
}