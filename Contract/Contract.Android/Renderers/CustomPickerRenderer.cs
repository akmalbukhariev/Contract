using System;
 
using Android.Graphics;
using Android.Graphics.Drawables;
 
using Android.Views;
using AndroidX.Core.Content;
using Contract.Control;
using Contract.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]

namespace Contract.Droid.Renderers
{
    [Obsolete]
    public class CustomPickerRenderer : PickerRenderer
    {
        CustomPicker element;

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);
            element = (CustomPicker)this.Element;

            if (Control != null && element != null)
            {
                Control.Gravity = GravityFlags.CenterHorizontal;
                
                if (element.ChartSyle)
                {
                    Control.Background = AddPickerStyles(element.Image);
                }
                else
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetStroke(0, Android.Graphics.Color.Transparent);
                    Control.SetBackground(gd);
                }
            }
        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            ShapeDrawable border = new ShapeDrawable();
            border.Paint.Color = Android.Graphics.Color.Black; 
            border.SetPadding(10, 10, 30, 10); 
            border.Paint.SetStyle(Paint.Style.Stroke);

            Drawable[] layers = { border, GetDrawable(imagePath) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);
            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

        private BitmapDrawable GetDrawable(string imagePath)
        {
            int resID = Resources.GetIdentifier(imagePath.ToLower(), "drawable", this.Context.PackageName);
            var drawable = ContextCompat.GetDrawable(this.Context, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;

            var result = new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, 30, 30, true));
            result.Gravity = Android.Views.GravityFlags.Right;

            return result;
        }
    }
}