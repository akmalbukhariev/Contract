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
    public partial class ViewEditContractButton : ContentView
    {
        #region Text
        public static readonly BindableProperty TextAddProperty =
            BindableProperty.Create(nameof(TextAdd),
                                    typeof(string),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string TextAdd
        {
            get { return (string)GetValue(TextAddProperty); }
            set { SetValue(TextAddProperty, value); }
        }

        public static readonly BindableProperty TextDeleteProperty =
            BindableProperty.Create(nameof(TextDelete),
                                    typeof(string),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string TextDelete
        {
            get { return (string)GetValue(TextDeleteProperty); }
            set { SetValue(TextDeleteProperty, value); }
        }
        #endregion

        #region Bacground Color
        public static readonly BindableProperty ButtonBackgroundColorProperty =
            BindableProperty.Create(nameof(ButtonBackgroundColor),
                                    typeof(Color),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public Color ButtonBackgroundColor
        {
            get { return (Color)GetValue(ButtonBackgroundColorProperty); }
            set { SetValue(ButtonBackgroundColorProperty, value); }
        }
        #endregion

        #region Is visible add button
        public static readonly BindableProperty IsVisibleAddProperty =
            BindableProperty.Create(nameof(IsVisibleAdd),
                                    typeof(bool),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public bool IsVisibleAdd
        {
            get { return (bool)GetValue(IsVisibleAddProperty); }
            set { SetValue(IsVisibleAddProperty, value); }
        }
        #endregion

        #region Is visible delete button
        public static readonly BindableProperty IsVisibleDeleteProperty =
            BindableProperty.Create(nameof(IsVisibleDelete),
                                    typeof(bool),
                                    typeof(ViewEditContractButton),
                                    defaultBindingMode: BindingMode.TwoWay);

        public bool IsVisibleDelete
        {
            get { return (bool)GetValue(IsVisibleDeleteProperty); }
            set { SetValue(IsVisibleDeleteProperty, value); }
        }
        #endregion

        public ViewEditContractButton()
        {
            InitializeComponent();

            this.lbTextAdd.SetBinding(Label.TextProperty, new Binding(nameof(TextAdd), source: this));
            this.lbTextDelete.SetBinding(Label.TextProperty, new Binding(nameof(TextDelete), source: this));
            this.boxView.SetBinding(BoxView.BackgroundColorProperty, new Binding(nameof(ButtonBackgroundColor), source: this));
            this.gridAdd.SetBinding(Grid.IsVisibleProperty, new Binding(nameof(IsVisibleAdd), source: this));
            this.gridDelete.SetBinding(Grid.IsVisibleProperty, new Binding(nameof(IsVisibleDelete), source: this));
        } 
    }
}