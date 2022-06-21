using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.Control
{
    public class MyCheckbox : StackLayout
    {
        public event EventHandler CheckedChanged;
        private readonly Image _image;

        private readonly Label _label;
        //CHANGE THESE 2 STRINGS ACCORDING TO YOUR NAMESPACE AND IMAGE

        static string imgUnchecked = "Tutorial02.Images.unchecked.png";
        static string imgChecked = "Tutorial02.Images.checked.png";
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(ICommand), typeof(MyCheckbox));

        public static readonly BindableProperty CheckedProperty = BindableProperty.Create("Checked", typeof(Boolean?), typeof(MyCheckbox), null,
            BindingMode.TwoWay, propertyChanged: CheckedValueChanged);

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create("Text", typeof(String), typeof(MyCheckbox), null, BindingMode.TwoWay, propertyChanged: TextValueChanged);

        public ICommand Command
        {
            get => (ICommand)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public Boolean? Checked
        {
            get => (bool?)GetValue(CheckedProperty);
            set => SetValue(CheckedProperty, value);
        }

        public String Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public MyCheckbox()
        {
            Orientation = StackOrientation.Horizontal;
            BackgroundColor = Color.Transparent;
            _image = new Image()
            { Source = ImageSource.FromResource(imgUnchecked), HeightRequest = 35, WidthRequest = 35, VerticalOptions = LayoutOptions.Center };
            var tg = new TapGestureRecognizer();
            tg.Tapped += Tg_Tapped;
            _image.GestureRecognizers.Add(tg);
            Children.Add(_image);
            _label = new Label() { VerticalOptions = LayoutOptions.Center };
            _label.GestureRecognizers.Add(tg);
            Children.Add(_label);
        }

        private void Tg_Tapped(object sender, EventArgs e)
        {
            Checked = !Checked;
        }

        private static void CheckedValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null && (Boolean)newValue) ((MyCheckbox)bindable)._image.Source = ImageSource.FromResource(imgChecked);
            else ((MyCheckbox)bindable)._image.Source = ImageSource.FromResource(imgUnchecked);
            ((MyCheckbox)bindable).CheckedChanged?.Invoke(bindable, EventArgs.Empty);
            ((MyCheckbox)bindable).Command?.Execute(null);
        }

        private static void TextValueChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue != null) ((MyCheckbox)bindable)._label.Text = newValue.ToString();
        }
    }
}
