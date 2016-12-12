using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Api.Models.ViewModels;
using MvvmCross.Plugins.Messenger;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooodPoint_Seller.Core.Messeges
{
    class AddictiveAcceptedMessage : MvxMessage
    {
        public AddictiveAcceptedMessage(object sender,  object item)
            : base(sender)
        {
            Additive = (ProductForOrder)item;
        }

        public ProductForOrder Additive { get; set; }
    
    }
}
