using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Contract.Control
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ChartSyleProperty =
            BindableProperty.Create(nameof(ChartSyle), typeof(bool), typeof(CustomPicker), false);

        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public CustomPicker()
        {
            TitleColor = Color.Black;
            TextColor = Color.Black;
        }

        public bool ChartSyle
        {
            get { return (bool)GetValue(ChartSyleProperty); }
            set { SetValue(ChartSyleProperty, value); }
        }

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }
    }
}
