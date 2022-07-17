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
    public partial class ViewPassword : ContentView
    {
        #region Title
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title),
                                    typeof(string),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region Text
        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(nameof(Text),
                                    typeof(string),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        } 
        #endregion

        public ViewPassword()
        {
            InitializeComponent();

            this.lbTitle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            this.enText.Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), source: this));
        }
    }
}