
using Contract.Model;
using Contract.Pages.SignUp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.SignUp
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
                Text1 = "ISTALGAN JOYDA \n SHARTNOMA TUZISH",
                Text2 = "Ofisda bo'lmang, bug'galterni kutmang. \n O'zingizga qulay vaqtda, qulay joyda \n shartnomalarni tuzing."
            };
            IntroductionInfo intro2 = new IntroductionInfo()
            {
                ImagePath = "intro_2",
                Text1 = "TAYYOR SHARTNOMANI ONSON \n JO'NATISH",
                Text2 = "Shartnomalarni qo'lda olib borish \n shart emas. Ularni istalgan messenjer \n orqali PDF formatida yuvorish."
            };
            IntroductionInfo intro3 = new IntroductionInfo()
            {
                ImagePath = "intro_3",
                Text1 = "QISQA MUDDATDA \n SHARTNOMA TUZISH",
                Text2 = "Endi shartnoma tuzish ko'p vaqt olmaydi. \n Shunchaki asosiy ma'lumotlarni \n to'ldiring va shartnoma tayyor."
            };
            IntroductionInfo intro4 = new IntroductionInfo()
            {
                ImagePath = "intro_4",
                Text1 = "QULAY SHABLONLAR \n YARATISH",
                Text2 = "Shartnoma tuzish uchu o'z huquqlar, \n talablar va imidjingizga mos \n shablonlarni yaratib, \n ulardan onson foydalaning."
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
