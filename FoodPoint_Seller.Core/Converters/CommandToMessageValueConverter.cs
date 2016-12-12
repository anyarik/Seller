using FooodPoint_Seller.Core.Messeges;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;
using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace FoodPoint_Seller.Core.Converters
{
    public class CommandToMessageValueConverter : MvxValueConverter<string, ICommand>
    {
        protected override ICommand Convert(string typeName, Type targetType, object parameter, CultureInfo culture)
        {
            return new MvxCommand(() =>
            {
                var messenger = Mvx.Resolve<IMvxMessenger>();
                var message = (MvxMessage)Activator.CreateInstance(Type.GetType(typeName), this, parameter);
                messenger.Publish(message);
            });
        }
    }
}
