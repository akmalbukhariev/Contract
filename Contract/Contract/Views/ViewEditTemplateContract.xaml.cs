﻿using System;
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

        #region Command
        public static readonly BindableProperty CommandClickTitleProperty =
            BindableProperty.Create(nameof(CommandClickTitle),
                                    typeof(ICommand),
                                    typeof(ViewEditTemplateContract),
                                    null);
         
        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter),
                                    typeof(object),
                                    typeof(ViewConfirm),
                                    null);

        public ICommand CommandClickTitle
        {
            get { return (ICommand)GetValue(CommandClickTitleProperty); }
            set { SetValue(CommandClickTitleProperty, value); }
        }
         
        public object CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }
        #endregion

        public ViewEditTemplateContract()
        {
            InitializeComponent();

            this.lbTitle.SetBinding(Label.TextProperty, new Binding(nameof(Title), source: this));
            this.edDescription.SetBinding(Editor.TextProperty, new Binding(nameof(Description), source: this));
        }

        public static void Execute(ICommand command)
        {
            if (command == null) return;
            if (command.CanExecute(null))
            {
                command.Execute(null);
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            boxViewTitle.BackgroundColor = Color.Gray;
            await Task.Delay(200);
        
            boxViewTitle.BackgroundColor = Color.FromHex("#F7F6F6");
            await Task.Delay(200);
             
            if (CommandClickTitle != null && CommandClickTitle.CanExecute(CommandParameter))
            {
                CommandClickTitle.Execute(CommandParameter);
            }
        }
    }
}