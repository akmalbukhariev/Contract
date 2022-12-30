using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Contract.Control;
using Contract.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]

namespace Contract.Droid.Renderers
{
    public class CustomViewCellRenderer : ViewCellRenderer
    {
        private Android.Views.View cellCoreView;
        private Drawable unSelectedBackground;
        private bool isSelected;

        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            this.cellCoreView = base.GetCellCore(item, convertView, parent, context);

            this.isSelected = false;
            this.unSelectedBackground = cellCoreView.Background;

            return cellCoreView;
        }

        protected override void OnCellPropertyChanged(object sender, PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);

            if (args.PropertyName == "IsSelected")
            {
                this.isSelected = !this.isSelected;

                if (this.isSelected)
                {
                    var extendedViewCell = sender as CustomViewCell;
                    this.cellCoreView.SetBackgroundColor(extendedViewCell.SelectedBackgroundColor.ToAndroid());
                }
                else
                {
                    this.cellCoreView.SetBackground(this.unSelectedBackground);
                }
            }
        }
    }
}