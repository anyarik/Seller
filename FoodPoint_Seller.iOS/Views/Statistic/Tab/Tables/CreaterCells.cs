using CoreGraphics;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.iOS.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace FoodPoint_Seller.Touch.Views.Statistic.Tab.Tables
{
    public class CreaterCells<TCell, TCellModel>
        where TCell : MvxTableViewCell
    {
        private List<string> fieldsTModel;
        private TCell baseViewCell;
        
        public CreaterCells(TCell baseViewCell)
        {
            this.baseViewCell = baseViewCell;

            this.fieldsTModel = typeof(TCellModel).GetFields()
                                              .Select(fi => fi.Name)
                                              .ToList();
        }
        /// <summary>
        /// По TCellModel создает Cells и биндит их с данными.
        /// </summary>
        /// <returns>
        /// Вернет View с колонками, количество определяется количеством полей в TCellModel.
        /// Стилизовать внутри функции.
        /// </returns>
        public UIView CreateCells()
        {
            var view = new UIView();

            for (int i = 0; i < fieldsTModel.Count; i++)
            {
                var itemLabel = new UILabel(new CGRect(1 + 80 * i, 1, 80, 20));
                this.CreateStyle(itemLabel);

                view.Add(itemLabel);

                var j = i;
                this.baseViewCell.DelayBind(
                        () =>
                        {
                            var set = this.baseViewCell.CreateBindingSet<TCell, TCellModel>();
                            set.Bind(itemLabel).To(this.fieldsTModel[j]);
                            set.Apply();
                        });

                #region Пример использования замыкания в DelayBind()

                //this.self.DelayBind(
                //        ((Func<int, Action>)((ind) =>
                //              () =>
                //             {
                //                 var set = this.self.CreateBindingSet<TCell, TModel>();
                //                 set.Bind(itemLabel).To(this.fieldsTModel[ind]);
                //                 set.Apply();
                //             }
                //            ))(i)
                //    ); 
                #endregion

            }

            return view;
        }

        private UILabel CreateStyle(UILabel label)
        {

            label.BackgroundColor = UIColor.Brown;
            label.Layer.BorderColor = UIColor.LightGray.CGColor;
            label.Layer.BorderWidth = 1;
            label.TextColor = UIColor.White;

            return label;
        }
    }
}
