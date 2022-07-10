
using Contract.Model;
using Contract.Pages.Introduction;
using Contract.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Introduction
{
    public class PageIntroductionViewModel : BaseModel
    {
        public ObservableCollection<IntroductionInfo> Images { get; set; }
         
        public IntroductionInfo CurrentItem { get; set; }
        public PageIntroductionViewModel(INavigation navigation) : base(navigation)
        {
            Images = new ObservableCollection<IntroductionInfo>();
            
            IntroductionInfo intro1 = new IntroductionInfo()
            {
                ImagePath = "intro_1",
                Text1 = RSC.Intro1_1,
                Text2 = RSC.Intro1_2
            };
            IntroductionInfo intro2 = new IntroductionInfo()
            {
                ImagePath = "intro_2",
                Text1 = RSC.Intro2_1,
                Text2 = RSC.Intro2_2
            };
            IntroductionInfo intro3 = new IntroductionInfo()
            {
                ImagePath = "intro_3",
                Text1 = RSC.Intro3_1,
                Text2 = RSC.Intro3_2
            };
            IntroductionInfo intro4 = new IntroductionInfo()
            {
                ImagePath = "intro_4",
                Text1 = RSC.Intro4_1,
                Text2 = RSC.Intro4_2
            };

            Images.Add(intro1);
            Images.Add(intro2);
            Images.Add(intro3);
            Images.Add(intro4);
        }
         
        public ICommand CommandClickNext => new Command(ClickNext);

        async void ClickNext()
        {
            await Navigation.PushAsync(new PageLoginInfo());
        }
    }
}
