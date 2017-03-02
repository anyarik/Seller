using System;
using MvvmCross.Binding.iOS.Views;
using Foundation;
using UIKit;
using CoreGraphics;

namespace FoodPoint_Seller.Touch.Views
{
    [Register("CustomerCell")]
    public partial class CustomerCell : MvxTableViewCell
    {
        private const string BindingText = "date date;newCumstomersNoSubs newCumstomersNoSubs;newCustomersSubs newCustomersSubs;oldCumstomersNoSubs oldCumstomersNoSubs;oldCustomersSubs oldCustomersSubs";

        public CustomerCell()
            : base(BindingText)
        {
        }



        public CustomerCell(IntPtr handle)
            : base(BindingText, handle)
        {

        }

        public string date { get; set; }
        public string newCumstomersNoSubs { get; set; }
        public string newCustomersSubs { get; set; }
        public string oldCumstomersNoSubs { get; set; }
        public string oldCustomersSubs { get; set; }



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

        public override UIView ViewForBaselineLayout
        {
            get
            {
                var view = new UIView();
                var dateLabel = new UILabel(new CGRect(1, 1, 20, 20));
                dateLabel.Text = date;
                dateLabel.BackgroundColor = UIColor.Black;

                view.Add(dateLabel);

                return view;
            }
        }

     
    }
}