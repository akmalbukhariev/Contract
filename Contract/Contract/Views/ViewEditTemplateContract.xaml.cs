using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewEditTemplateContract : ContentView
    {   
        #region Title
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(nameof(Title),
                                    typeof(string),
                                    typeof(ViewEditTemplateContract),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }
        #endregion

        #region Description
        public static readonly BindableProperty DescriptionProperty =
            BindableProperty.Create(nameof(Description),
                                    typeof(string),
                                    typeof(ViewEditTemplateContract),
                                    defaultBindingMode: BindingMode.TwoWay);

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }
        #endregion

        #region Delete
        public static readonly BindableProperty IsVisibleDeleteProperty =
            BindableProperty.Create(nameof(IsVisibleDelete),
                                    typeof(bool),
                                    typeof(ViewEditTemplateContract),
                                    defaultBindingMode: BindingMode.TwoWay);

        public bool IsVisibleDelete
        {
            get { return (bool)GetValue(IsVisibleDeleteProperty); }
            set { SetValue(IsVisibleDeleteProperty, value); }
        }
        #endregion

        #region Command Title
        public static readonly BindableProperty CommandClickTitleProperty =
            BindableProperty.Create(nameof(CommandClickTitle),
                                    typeof(ICommand),
                                    typeof(ViewEditTemplateContract),
                                    null);

        public ICommand CommandClickTitle
        {
            get { return (ICommand)GetValue(CommandClickTitleProperty); }
            set { SetValue(CommandClickTitleProperty, value); }
        }

        public static readonly BindableProperty CommandClickTitleParameterProperty =
            BindableProperty.Create(nameof(CommandClickTitleParameter),
                                    typeof(object),
                                    typeof(ViewConfirm),
                                    null);

        public object CommandClickTitleParameter
        {
            get => GetValue(CommandClickTitleParameterProperty);
            set => SetValue(CommandClickTitleParameterProperty, value);
        }

        #endregion

        #region Command Text
        public static readonly BindableProperty CommandClickTextProperty =
            BindableProperty.Create(nameof(CommandClickText),
                                    typeof(ICommand),
                                    typeof(ViewEditTemplateContract),
                                    null);

        public ICommand CommandClickText
        {
            get { return (ICommand)GetValue(CommandClickTextProperty); }
            set { SetValue(CommandClickTextProperty, value); }
        }

        public static readonly BindableProperty CommandClickTextParameterProperty =
            BindableProperty.Create(nameof(CommandClickTextParameter),
                                    typeof(object),
                                    typeof(ViewConfirm),
                                    null);
          
        public object CommandClickTextParameter
        {
            get => GetValue(CommandClickTextParameterProperty);
            set => SetValue(CommandClickTextParameterProperty, value);
        }
        #endregion

        #region Delete command
        public static readonly BindableProperty CommandDeleteProperty =
            BindableProperty.Create(nameof(CommandDelete),
                                    typeof(ICommand),
                                    typeof(ViewEditTemplateContract),
                                    null);

        public ICommand CommandDelete
        {
            get { return (ICommand)GetValue(CommandDeleteProperty); }
            set { SetValue(CommandDeleteProperty, value); }
        }

        public static readonly BindableProperty CommandDeleteParameterProperty =
            BindableProperty.Create(nameof(CommandDeleteParameter),
                                    typeof(object),
                                    typeof(ViewConfirm),
                                    null);

        public object CommandDeleteParameter
        {
            get => GetValue(CommandDeleteParameterProperty);
            set => SetValue(CommandDeleteParameterProperty, value);
        }
        #endregion

        public Label Label
        {
            get => lbDescription;
            set => lbDescription = value;
        }

        public void ShowLabelText(bool show)
        {
            lbDescription.IsVisible = show;
            edDescription.IsVisible = !show;
        }

        public ViewEditTemplateContract()
        {
            InitializeComponent();

            edDescription.IsVisible = false;
            this.lbTitle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            this.lbDescription.SetBinding(Label.TextProperty, new Binding(nameof(Description), source: this));
            this.imDelete.SetBinding(Image.IsVisibleProperty, new Binding(nameof(IsVisibleDelete), source: this));
        }

        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private async void Title_Tapped(object sender, EventArgs e)
        {
            return;
            boxViewTitle.BackgroundColor = Color.Gray;
            await Task.Delay(200);
        
            boxViewTitle.BackgroundColor = Color.FromHex("#F7F6F6");
            await Task.Delay(200);
             
            if (CommandClickTitle != null && CommandClickTitle.CanExecute(CommandClickTitleParameter))
            {
                CommandClickTitle.Execute(CommandClickTitleParameter);
            }
        }

        private async void Text_Tapped(object sender, EventArgs e)
        {
            await this.ScaleTo(0.8, 200);
            await this.ScaleTo(1, 200, Easing.SpringOut);

            if (CommandClickText != null && CommandClickText.CanExecute(CommandClickTextParameter))
            {
                CommandClickText.Execute(CommandClickTextParameter);
            }
        }

        private void Delete_Tapped(object sender, EventArgs e)
        { 
            if (CommandDelete != null && CommandDelete.CanExecute(CommandDeleteParameter))
            {
                CommandDelete.Execute(CommandDeleteParameter);
            }
        }
    }
}