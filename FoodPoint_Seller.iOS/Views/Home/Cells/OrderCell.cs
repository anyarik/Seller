using System;
using MvvmCross.Binding.iOS.Views;
using Foundation;
using UIKit;
using CoreGraphics;


namespace FoodPoint_Seller.Touch.Views.Home.Cells
{
    public partial class OrderCell: MvxTableViewCell
    {
        private const string BindingText = "ID ID";
        //private MvxImageViewLoader _imageHelper;

        public OrderCell()
            : base(BindingText)
        { 
        }

        public OrderCell(IntPtr handle)
            : base(BindingText, handle)
        {
        }

        public string ID
        {
            get { return IdLabel.Text; }
            set { IdLabel.Text = value; }
        }

        //public string OrderedFood
        //{
        //    get { return _imageHelper.ImageUrl; }
        //    set { _imageHelper.ImageUrl = value; }
        //}

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

        private void InitialiseImageHelper()
        {
            //_imageHelper = new MvxImageViewLoader(() => KittenImageView);
        }
    }
}