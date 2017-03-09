using Android.Content;
using System.Collections.Generic;
using System.Reflection;

using FoodPoint_Seller.Droid.Services;
using MvvmCross.Droid.Platform;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Views;
using MvvmCross.Platform;
using MvvmCross.Droid.Shared.Presenter;
using FoodPoint_Seller.Droid.Utilities;
using MvvmCross.Platform.Droid.Platform;
using FoodPoint_Seller.Core.Services.Implementations;
using MvvmCross.Dialog.Droid;
using FoodPoint_Seller.Core.Services;
using MvvmCross.Binding.Bindings.Target.Construction;
using Android.Views;
using FoodPoint_Seller.Droid.MvxBinding;

namespace FoodPoint_Seller.Droid
{
    public class Setup : MvxAndroidDialogSetup
    {
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();

        }


        protected override IEnumerable<Assembly> AndroidViewAssemblies => new List<Assembly>(base.AndroidViewAssemblies)
        {
            typeof(Android.Support.Design.Widget.NavigationView).Assembly,
            typeof(Android.Support.Design.Widget.FloatingActionButton).Assembly,
            typeof(Android.Support.V7.Widget.Toolbar).Assembly,
            typeof(Android.Support.V4.Widget.DrawerLayout).Assembly,
            typeof(Android.Support.V4.View.ViewPager).Assembly,
            typeof(MvvmCross.Droid.Support.V7.RecyclerView.MvxRecyclerView).Assembly
        };

        /// <summary>
        /// This is very important to override. The default view presenter does not know how to show fragments!
        /// </summary>
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var mvxFragmentsPresenter = new MvxFragmentsPresenter(AndroidViewAssemblies);
            Mvx.RegisterSingleton<IMvxAndroidViewPresenter>(mvxFragmentsPresenter);

            //add a presentation hint handler to listen for pop to root
            mvxFragmentsPresenter.AddPresentationHintHandler<MvxPanelPopToRootPresentationHint>(hint =>
            {                
                var activity = Mvx.Resolve<IMvxAndroidCurrentTopActivity>().Activity;
                var fragmentActivity = activity as Android.Support.V4.App.FragmentActivity;
             
                for (int i = 0; i < fragmentActivity.SupportFragmentManager.BackStackEntryCount; i++)
                {
                    fragmentActivity.SupportFragmentManager.PopBackStack();
                }
                return true;
            });
            //register the presentation hint to pop to root
            //picked up in the third view model
            Mvx.RegisterSingleton<MvxPresentationHint>(() => new MvxPanelPopToRootPresentationHint());            
            return mvxFragmentsPresenter;
        }

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IDialogService>(() => new DialogService());
        }
        protected override void FillTargetFactories(IMvxTargetBindingFactoryRegistry registry)
        {
            registry.RegisterCustomBindingFactory<View>(
                "MyHeight",
                view => new LayoutHeightBinding(view)
                );


            base.FillTargetFactories(registry);
        }
    }
}