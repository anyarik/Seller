using CoreGraphics;
using FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables;
using Foundation;
using MvvmCross.Binding.iOS.Views;
using System;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables
{
    public class BaseViewCell<TCell, TCellModel>: MvxTableViewCell 
        where TCell : MvxTableViewCell
    {
        public static readonly UINib Nib = UINib.FromName(nameof(TCell), NSBundle.MainBundle);
        public static readonly NSString Key = new NSString(nameof(TCell));

        public BaseViewCell(IntPtr handle) : base (handle)
        {

            try
            {
                var createrCells = new CreaterCells<TCell, TCellModel>(this as TCell);

                var view = createrCells.CreateCells();

                this.AddSubview(view);
            }
            catch (Exception exp)
            {

                throw new Exception("В классе BaseViewCell this = null.");
            }

        }

        public static TCell Create()
        {
            return (TCell)Nib.Instantiate(null, null)[0];
        }


        public static float GetCellHeight()
        {
            return 120f;
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            AnimateToScale(1.2f);
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            AnimateToScale(1.0f);
        }

        public override void TouchesEnded(NSSet touches, UIEvent evt)
        {
            AnimateToScale(1.0f);
        }

        private void AnimateToScale(float scale)
        {
            UIView.BeginAnimations("animateToScale");
            UIView.SetAnimationCurve(UIViewAnimationCurve.EaseIn);
            UIView.SetAnimationDuration(0.5);
            Transform = CGAffineTransform.MakeScale(scale, 1.0f);
            UIView.CommitAnimations();
        }

    }
}