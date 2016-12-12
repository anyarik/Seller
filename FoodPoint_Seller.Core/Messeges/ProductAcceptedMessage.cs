using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooodPoint_Seller.Core.Messeges
{
    class ProductAcceptedMessage : MvxMessage
    {
        public ProductAcceptedMessage(object sender,  object item)
            : base(sender)
        {
            Product = (ProductForOrder)item;
        }

        public ProductForOrder Product { get; set; }
    
    }
}
