using Contract.Control;
using Contract.Model;
using Contract.ViewModel.Introduction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.Introduction
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageIntroduction : IPage
    { 
        public PageIntroduction()
        {
            InitializeComponent(); 
            SetModel(new PageIntroductionViewModel(Navigation));

            grButton.IsVisible = false;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Model.Parent = Parent;
        }

        private async void Skip_Tapped(object sender, EventArgs e)
        { 
            Model.SetTransitionType();
            await Navigation.PushAsync(new PageLoginInfo()); 
        }

        private void Enter_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Grid)sender);

            Application.Current.MainPage = new TransitionNavigationPage(new Login.PageLogin()); 
        }

        private void CarouselView_CurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
        {
            IntroductionInfo previousItem = e.PreviousItem as IntroductionInfo;
            IntroductionInfo currentItem = e.CurrentItem as IntroductionInfo;

            if (previousItem != null && previousItem.ImagePath == PageIntroductionViewModel.Intro_5)
            {
                grButton.IsVisible = true;
                indicatorview.IsVisible = false;
            }
            else
            {
                grButton.IsVisible = false;
                indicatorview.IsVisible = true;
            }
        }

        private void CarouselView_PositionChanged(object sender, PositionChangedEventArgs e)
        {
            if (e.CurrentPosition == 4)
            {
                grButton.IsVisible = true;
                indicatorview.IsVisible = false;
            }
            else
            {
                grButton.IsVisible = false;
                indicatorview.IsVisible = true;
            }

            //Console.WriteLine($"Position:  {e.CurrentPosition}");
        }
    }
}