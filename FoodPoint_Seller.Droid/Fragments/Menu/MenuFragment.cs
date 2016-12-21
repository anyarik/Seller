using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using FoodPoint_Seller.Core.ViewModels;
using FoodPoint_Seller.Droid.Activities;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;

namespace FoodPoint_Seller.Droid.Fragments
{
	[MvxFragment(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("foodpoint_seller.droid.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var ignore = base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_navigation, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            _navigationView.Menu.FindItem(Resource.Id.nav_home).SetChecked(true);

            return view;
        }

        public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (item != _previousMenuItem)
            {
                _previousMenuItem?.SetChecked(false);
            }
            
            item.SetCheckable(true);
            item.SetChecked(true);

            _previousMenuItem = item;

            Navigate (item.ItemId);

            return true;
        }

        private async Task Navigate(int itemId)
        {
            ((MainActivity)Activity).DrawerLayout.CloseDrawers ();

            // add a small delay to prevent any UI issues
            await Task.Delay (TimeSpan.FromMilliseconds (250));

            switch (itemId) 
            {
                case Resource.Id.nav_home:
                    ViewModel.ShowHomeCommand.Execute();
                    break;
                case Resource.Id.nav_statistic_owner:
                    ViewModel.ShowStatisticOwnerCommand.Execute();
                    break;
                case Resource.Id.nav_settings:
                    ViewModel.ShowRecyclerCommand.Execute();
                    break;
                case Resource.Id.nav_statistic_seller:
                    ViewModel.ShowStatisticSellerCommand.Execute();
                    break;
                case Resource.Id.nav_helpfeedback:
                    ViewModel.ShowHelpCommand.Execute();
                    break;
            }
        }
    }
}
