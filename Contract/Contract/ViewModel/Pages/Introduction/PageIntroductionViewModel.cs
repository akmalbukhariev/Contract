﻿
using Contract.Model;
using Contract.Pages.Introduction;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Introduction
{
    public class PageIntroductionViewModel : BaseModel
    {
        public const string Intro_1 = "intro_1";
        public const string Intro_2 = "intro_2";
        public const string Intro_3 = "intro_3";
        public const string Intro_4 = "intro_4";
        public const string Intro_5 = "intro_5";
        public ObservableCollection<IntroductionInfo> Images { get; set; }
         
        public IntroductionInfo CurrentItem { get; set; }
        public PageIntroductionViewModel(INavigation navigation) : base(navigation)
        {
            Images = new ObservableCollection<IntroductionInfo>();
            
            IntroductionInfo intro1 = new IntroductionInfo()
            {
                ImagePath = Intro_1,
                Text1 = RSC.Intro1_1,
                Text2 = RSC.Intro1_2
            };
            IntroductionInfo intro2 = new IntroductionInfo()
            {
                ImagePath = Intro_2,
                Text1 = RSC.Intro2_1,
                Text2 = RSC.Intro2_2
            };
            IntroductionInfo intro3 = new IntroductionInfo()
            {
                ImagePath = Intro_3,
                Text1 = RSC.Intro3_1,
                Text2 = RSC.Intro3_2
            };
            IntroductionInfo intro4 = new IntroductionInfo()
            {
                ImagePath = Intro_4,
                Text1 = RSC.Intro4_1,
                Text2 = RSC.Intro4_2
            };
            IntroductionInfo intro5 = new IntroductionInfo()
            {
                ImagePath = Intro_5,
                Text1 = RSC.LoginInfo1_1,
                Text2 = RSC.LoginInfo1_2,
                ShowButton = true
            };

            Images.Add(intro1);
            Images.Add(intro2);
            Images.Add(intro3);
            Images.Add(intro4);
            Images.Add(intro5);
        }
         
        public ICommand CommandClickNext => new Command(ClickNext);

        async void ClickNext()
        {
            await Navigation.PushAsync(new PageLoginInfo());
        }
    }
}
