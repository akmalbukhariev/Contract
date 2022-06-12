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
    public partial class ViewShowContractCount : ContentView
    {
        #region Image
        public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource),
                                                                    typeof(string),
                                                                    typeof(ViewShowContractCount),
                                                                    null,
                                                                    propertyChanged: ImageSourcePropertyChanged);
        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        private static void ImageSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (ViewShowContractCount)bindable;
            if (newValue != null)
                control.image.Source = newValue.ToString();
        }
        #endregion

        #region Text
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(ViewShowContractCount),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        #endregion

        #region Text count
        public static readonly BindableProperty TextCountProperty =
            BindableProperty.Create(nameof(TextCount),
                                    typeof(string),
                                    typeof(ViewShowContractCount),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string TextCount
        {
            get { return (string)GetValue(TextCountProperty); }
            set { SetValue(TextCountProperty, value); }
        }
        #endregion

        public ViewShowContractCount()
        {
            InitializeComponent();
            this.label.SetBinding(Label.TextProperty, new Binding(nameof(Text), source: this));
            this.labelCount.SetBinding(Label.TextProperty, new Binding(nameof(TextCount), source: this));
        }
    }
}