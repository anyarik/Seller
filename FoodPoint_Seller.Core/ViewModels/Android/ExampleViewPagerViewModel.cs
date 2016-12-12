namespace FoodPoint_Seller.Core.ViewModels
{
	public class ExampleViewPagerViewModel : BaseViewModel
    {
        public RecyclerViewModel Recycler { get; private set; }

        public ExampleViewPagerViewModel()
        {
            Recycler = new RecyclerViewModel();
        }
    }
}