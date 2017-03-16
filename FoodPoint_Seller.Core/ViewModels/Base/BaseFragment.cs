using FoodPoint_Seller.Api.Controllers;
using FoodPoint_Seller.Api.Models.DomainModels;
using FoodPoint_Seller.Core.Services;
using Meowtrix.ITask;
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
        private ISellerOrderService _sellerOrderService;
        private ISellerAuthService _authService;

        public INC<string> TextStatusSeller;
        public INC<string> TextToolbarBtn = new NC<string>("", (e) =>
        {
        });

        public BaseFragment(ISellerOrderService sellerOrderService, ISellerAuthService authService)
        {
            this._sellerOrderService = sellerOrderService;
            this._authService = authService;
            this.TextStatusSeller =  new NC<string>(_sellerOrderService.CurentStatus, (e) =>
            {

            });

            initFragment();
        }

        private async void initFragment()
        {
            _sellerOrderService.ChangeStatus += SellerOrderService_ChangeStatus;
            _sellerOrderService.ChangeExitText += _sellerOrderService_ChangeExitText;

            TextToolbarBtn.Value = await GetToolbarText();
        }

        private async Task<string> GetToolbarText()
        {
            var seller = await  this._authService.GetProfile();
            var curentSeller = seller as SellerAccountModel;

            var orders = await _sellerOrderService.GetOrders();
            if (curentSeller.Busyness && orders.Count > 0)
            {
                return "Возобновить прием заказов";
            }
            else if (curentSeller.Busyness)
            {
                return "Возобносить прием заказов";
            }
            else if (orders.Count > 0)
            {
                return "Остановить прием заказов";
            }
            else 
            {
                return "Выйти";
            }
           
        }

        private void _sellerOrderService_ChangeExitText(object sender, string textButtonExit)
        {
            TextToolbarBtn.Value = textButtonExit;
        }

        private void SellerOrderService_ChangeStatus(object sender, string e)
        {
            this.TextStatusSeller.Value = e;
        }

        public async void OnClickOffline()
        {
            var order = await _sellerOrderService.GetOrders();
            if (order.Count == 0)
            {
                //this._orderController.HubDisconnect();
                this._authService.Logout();
                ShowViewModel<LoginViewModel>();
            }
            else
            {
                this._authService.ChangeStatusSeler();
                var seller = await this._authService.GetProfile();
                var curentSeller = seller as SellerAccountModel;

                if (curentSeller.Busyness)
                    TextToolbarBtn.Value = "Возобновить прием заказов";
                else
                    TextToolbarBtn.Value = "Остановить прием заказов";
            }
        }
    }
}
