using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.FieldBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodPoint_Seller.Core.ViewModels.Base
{
    public class BaseFragment:MvxViewModel
    {

        public INC<string> TextStatusSeller;

        private ISellerOrderService _sellerOrderService;

        public BaseFragment(ISellerOrderService sellerOrderService)
        {
            this._sellerOrderService = sellerOrderService;
            this.TextStatusSeller =  new NC<string>(_sellerOrderService.CurentStatus, (e) =>
            {

            });
            _sellerOrderService.ChangeStatus += SellerOrderService_ChangeStatus;
        }

        private void SellerOrderService_ChangeStatus(object sender, string e)
        {
            this.TextStatusSeller.Value = e;
        }

        public INC<string> TextActiveSeller = new NC<string>("Выйти", (e) =>
        {
        });
    }
}
