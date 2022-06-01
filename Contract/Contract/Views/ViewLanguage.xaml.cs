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
                                                                   null,
                                                                   propertyChanged: TextLanguagePropertyChanged);
        public string TextLanguage
        {
            get { return (string)GetValue(TextLanguageProperty); }
            set { SetValue(TextLanguageProperty, value); }
        }

        private static void TextLanguagePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewLanguage)bindable;
            if (newValue != null)
                control.label.Text = newValue.ToString();
        }
        #endregion

        #region Text Language Info
        public static readonly BindableProperty TextLanguageInfoProperty = BindableProperty.Create(nameof(TextLanguageInfo),
                                                                   typeof(string),
                                                                   typeof(ViewLanguage),
                                                                   null,
                                                                   propertyChanged: TextLanguageInfoPropertyChanged);
        public string TextLanguageInfo
        {
            get { return (string)GetValue(TextLanguageInfoProperty); }
            set { SetValue(TextLanguageInfoProperty, value); }
        }

        private static void TextLanguageInfoPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewLanguage)bindable;
            if (newValue != null)
                control.labelInfo.Text = newValue.ToString();
        }
        #endregion

        public ViewLanguage()
        {
            InitializeComponent();
        }
    }
}