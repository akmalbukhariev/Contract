using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewNavigationBar : ContentView
    {
        #region Image
        public static readonly BindableProperty FlagImageProperty = BindableProperty.Create(nameof(BackImage),
                                                                    typeof(string),
                                                                    typeof(ViewNavigationBar),
                                                                    null,
                                                                    propertyChanged: FlagImagePropertyChanged);
        public string BackImage
        {
            get { return (string)GetValue(FlagImageProperty); }
            set { SetValue(FlagImageProperty, value); }
        }

        private static void FlagImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewNavigationBar)bindable;
            if (newValue != null)
                control.image.Source = newValue.ToString();
        }
        #endregion

        public bool UseWhite { get; set; } = false;
        public ViewNavigationBar()
        {
            InitializeComponent();
        }

        private async void Back_Tapped(object sender, EventArgs e)
        {
            var im = (Image)sender;
            im.Source = UseWhite ? "back_left_gray" : "back_left_white";
            await Task.Delay(100);

            im.Source = UseWhite ? "back_left_white" : "back_left_gray";
            await Task.Delay(200);

            await Navigation.PopAsync();
        }
    }
}