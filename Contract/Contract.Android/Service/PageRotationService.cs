using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Contract.Droid.Service;
using Contract.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageRotationService))]

namespace Contract.Droid.Service
{
    public class PageRotationService : IRotationService
    {
        public void EnableRotation()
        {
            MainActivity.Instance.RequestedOrientation = Android.Content.PM.ScreenOrientation.Sensor;
        }

        public void DisableRotation()
        {
            MainActivity.Instance.RequestedOrientation = Android.Content.PM.ScreenOrientation.Portrait;
        }
    }
}