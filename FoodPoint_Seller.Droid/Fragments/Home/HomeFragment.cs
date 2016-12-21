using Android.Runtime;
using FoodPoint_Seller.Core.ViewModels;
using Android.OS;
using Android.Views;
using MvvmCross.Droid.Shared.Attributes;
using FoodPoint_Seller.Api.Controllers;
using MvvmCross.Platform;
using FoodPoint_Seller.Api.Controllers.Implementations;

namespace FoodPoint_Seller.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.content_frame)]
    [Register("foodpoint_seller.droid.fragments.HomeFragment")]
    public class HomeFragment : BaseFragment<HomeViewModel>
    {
        private IOrderController orderController;


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            ShowHamburgerMenu = true;
            return base.OnCreateView(inflater, container, savedInstanceState);

            this.orderController = Mvx.IocConstruct<OrderController>();
        }

        //public override void OnDestroy()
        //{
        //    base.OnDestroyView();
        //    //this.orderController.HubDisconnect();
        //}
 
        protected override int FragmentId => Resource.Layout.fragment_home;
    }
}