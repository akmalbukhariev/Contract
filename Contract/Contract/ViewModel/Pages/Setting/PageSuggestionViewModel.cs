﻿using LibContract.HttpModels;
using LibContract.HttpResponse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Contract.ViewModel.Pages.Setting
{
    public class PageSuggestionViewModel : BaseModel
    {
        public string SelectedOffer { get => GetValue<string>(); set => SetValue(value); }
        public string Description { get => GetValue<string>(); set => SetValue(value); }
        public List<string> OfferList { get => GetValue<List<string>>(); set => SetValue(value); }

        public PageSuggestionViewModel(INavigation navigation) : base(navigation)
        {
            SelectedOffer = "";
            Description = "";
            OfferList = new List<string>();
        }

        public ICommand CommandSend => new Command(Send);

        async void Send()
        {
            if (!ControlApp.InternetOk()) return;

            if (string.IsNullOrEmpty(SelectedOffer) || string.IsNullOrEmpty(Description))
            {
                await Application.Current.MainPage.DisplayAlert(RSC.Password, RSC.FieldEmpty, RSC.Ok);
                return;
            }

            OfferAndObjection request = new OfferAndObjection()
            {
                user_phone_number = ControlApp.UserInfo.phone_number,
                type = SelectedOffer,
                description = Description,
                created_date = DateTime.Now.ToString(Constants.TimeFormat)
            };

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseOfferObjection response = await Net.HttpService.SaveOfferObjection(request);
            ControlApp.CloseLoadingView();

            if (!ControlApp.CheckResponse(response)) return;

            string strMessage = response.result ? RSC.SuccessfullyCompleted : RSC.Failed;
            await Application.Current.MainPage.DisplayAlert(RSC.Settings, strMessage, RSC.Ok);

            if (response.result)
                await Navigation.PopAsync();
        }
    }
}
