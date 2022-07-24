﻿
using Contract.Control;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Contract.ViewModel
{
    public class BaseModel : INotifyPropertyChanged
    {
        public Element Parent { get; set; }

        protected INavigation Navigation { get; set; }
        protected ControlApp ControlApp => ControlApp.Instance;

        private Dictionary<string, object> PropertyList;

        public event PropertyChangedEventHandler PropertyChanged;

        public BaseModel(INavigation navigation)
        {
            Navigation = navigation;
        }

        public BaseModel()
        {
            
        }

        protected T GetValue<T>([CallerMemberName] string propertyName = "")
        {
            if (PropertyList == null)
                PropertyList = new Dictionary<string, object>();

            if (!PropertyList.ContainsKey(propertyName))
                return default;

            return (T)PropertyList[propertyName];
        }

        protected void SetValue(object value, [CallerMemberName] string propertyName = "")
        {
            if (PropertyList == null)
                PropertyList = new Dictionary<string, object>();

            if (PropertyList.ContainsKey(propertyName))
            {
                if (PropertyList[propertyName] == value) return;

                PropertyList[propertyName] = value;
            }
            else
            {
                PropertyList.Add(propertyName, value);
            }

            OnPropertyChanged(propertyName);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SetTransitionType(TransitionType transitionType = TransitionType.SlideFromRight)
        {
            if (Parent == null) return;

            var transitionNavigationPage = Parent as TransitionNavigationPage;
            if (transitionNavigationPage != null)
                transitionNavigationPage.TransitionType = transitionType;
        }

        public async void Back()
        {
            await Navigation.PopAsync();
        }
    }
}
