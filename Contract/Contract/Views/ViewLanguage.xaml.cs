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
    public partial class ViewLanguage : ContentView
    {
        #region Image
        public static readonly BindableProperty FlagImageProperty = BindableProperty.Create(nameof(FlagImage), 
                                                                    typeof(string), 
                                                                    typeof(ViewLanguage), 
                                                                    null, 
                                                                    propertyChanged: FlagImagePropertyChanged);
        public string FlagImage
        {
            get { return (string)GetValue(FlagImageProperty); }
            set { SetValue(FlagImageProperty, value); }
        }

        private static void FlagImagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewLanguage)bindable;
            if (newValue != null)
                control.image.Source = newValue.ToString();
        }
        #endregion

        #region Text Language
        public static readonly BindableProperty TextLanguageProperty = BindableProperty.Create(nameof(TextLanguage),
                                                                   typeof(string),
                                                                   typeof(ViewLanguage),
                                                                   defaultBindingMode: BindingMode.TwoWay);
        public string TextLanguage
        {
            get { return (string)GetValue(TextLanguageProperty); }
            set { SetValue(TextLanguageProperty, value); }
        } 
        #endregion

        #region Text Language Info
        public static readonly BindableProperty TextLanguageInfoProperty = BindableProperty.Create(nameof(TextLanguageInfo),
                                                                   typeof(string),
                                                                   typeof(ViewLanguage),
                                                                   defaultBindingMode: BindingMode.TwoWay);
        public string TextLanguageInfo
        {
            get { return (string)GetValue(TextLanguageInfoProperty); }
            set { SetValue(TextLanguageInfoProperty, value); }
        }
        #endregion

        public Label LabelLanguage => this.label;
        public Label LabelLanguageInfo => this.labelInfo;

        public ViewLanguage()
        {
            InitializeComponent();

            this.label.SetBinding(Label.TextProperty, new Binding(nameof(TextLanguage), source: this));
            this.labelInfo.SetBinding(Label.TextProperty, new Binding(nameof(TextLanguageInfo), source: this));
        }

        public void UnSelected()
        {
            label.TextColor = Color.Black;
            labelInfo.TextColor = Color.Gray;
            line.BackgroundColor = Color.LightGray;
        }

        public void Selected()
        {
            label.TextColor = Color.Red;
            labelInfo.TextColor = Color.LightPink;
            line.BackgroundColor = Color.LightPink;
        }
    }
}