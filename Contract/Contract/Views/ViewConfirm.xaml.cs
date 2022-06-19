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
    public partial class ViewConfirm : ContentView
    {
        #region Command
        public static readonly BindableProperty CommandCodeProperty =
            BindableProperty.Create(nameof(CommandCode),
                                    typeof(ICommand),
                                    typeof(ViewConfirm),
                                    null);

        public static readonly BindableProperty CommandERIProperty =
            BindableProperty.Create(nameof(CommandERI),
                                    typeof(ICommand),
                                    typeof(ViewConfirm),
                                    null);

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter),
                                    typeof(object),
                                    typeof(ViewConfirm),
                                    null);

        public ICommand CommandCode
        {
            get { return (ICommand)GetValue(CommandCodeProperty); }
            set { SetValue(CommandCodeProperty, value); }
        }

        public ICommand CommandERI
        {
            get { return (ICommand)GetValue(CommandERIProperty); }
            set { SetValue(CommandERIProperty, value); }
        }

        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        #endregion

        public ViewConfirm()
        {
            InitializeComponent();
        }

        private void Eye_Tapped(object sender, EventArgs e)
        {
            ChangeBack((Grid)sender);

            if (CommandCode != null && CommandCode.CanExecute(CommandParameter))
            {
                CommandCode.Execute(CommandParameter);
            }
        }

        private void Finger_Tapped(object sender, EventArgs e)
        {
            ChangeBack((Grid)sender);

            if (CommandERI != null && CommandERI.CanExecute(CommandParameter))
            {
                CommandERI.Execute(CommandParameter);
            }
        }

        async void ChangeBack(Grid grid)
        {
            grid.BackgroundColor = Color.Gray;
            await Task.Delay(100);

            grid.BackgroundColor= Color.White;
            await Task.Delay(200);
        }
    }
}