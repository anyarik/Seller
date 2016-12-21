namespace FoodPoint_Seller.Core.Services
{
    public interface IDialogService
    {
        void Alert(string message, string title, string okbtnText);
    }
}