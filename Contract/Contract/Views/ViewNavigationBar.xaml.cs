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

        #region Title
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title),
                                    typeof(string),
                                    typeof(ViewNavigationBar),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region Title color
        public static readonly BindableProperty TitleColorProperty =
            BindableProperty.Create(nameof(TitleColor),
                                    typeof(Color),
                                    typeof(ViewNavigationBar),
                                    defaultBindingMode: BindingMode.TwoWay);

        public Color TitleColor
        {
            get { return (Color)GetValue(TitleColorProperty); }
            set { SetValue(TitleColorProperty, value); }
        }

        //private static void TitleColorPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        //{
        //    var control = (ViewNavigationBar)bindable;
        //    if (newValue != null)
        //    {
        //        control.lbTitle.TextColor = (Color)newValue;
        //    }
        //}
        #endregion

        #region Font size  
        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize),
                                    typeof(int),
                                    typeof(ViewNavigationBar),
                                    defaultBindingMode: BindingMode.TwoWay);

        public int FontSize
        {
            get { return (int)GetValue(FontSizeProperty); }
            set { SetValue(FontSizeProperty, value); }
        }
        #endregion

        public bool UseWhite { get; set; } = false;
        public bool IsThisModalPage { get; set; } = false;
        public ViewNavigationBar()
        {
            InitializeComponent();

            this.lbTitle.SetBinding(Label.FontSizeProperty, new Binding(nameof(FontSize), source: this));
            this.lbTitle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            this.lbTitle.SetBinding(Label.TextColorProperty, new Binding(nameof(TitleColor), source: this));
        }

        private async void Back_Tapped(object sender, EventArgs e)
        {
            var im = (Image)sender;
            im.Source = UseWhite ? "back_left_gray" : "back_left_white";
            await Task.Delay(100);

            im.Source = UseWhite ? "back_left_white" : "back_left_gray";
            await Task.Delay(200);

            if (IsThisModalPage)
                await Navigation.PopModalAsync();
            else
                await Navigation.PopAsync();
        }
    }
}