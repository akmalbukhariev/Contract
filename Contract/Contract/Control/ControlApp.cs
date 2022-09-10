﻿using Acr.UserDialogs; 
using System; 
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Contract.Control
{
    public class ControlApp
    { 
        private bool _closeLoadingView = false; 
        public bool AppStarting { get; set; }
        public bool AppOnResume { get; set; }
        public bool AppOnSleep { get; set; }
        public string UserId { get; set; }
  
        private static ControlApp _instance = null;

        public Net.Login LoginInfo { get; set; } = new Net.Login();

        private ControlApp()
        {
           
        }

        public static ControlApp Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ControlApp();
                }

                return _instance;
            }
        }

        public bool IsInternetConnectionOk
        {
            get
            {
                bool ok = false;

                var current = Connectivity.NetworkAccess;

                if (current == NetworkAccess.Internet)
                {
                    ok = true;
                }

                return ok;
            }
        }

        public bool InternetOk()
        {
            if (!IsInternetConnectionOk)
            {
                //UserDialogs.Instance.Alert(RSC.NoInternet, RSC.Internet, RSC.Ok);
                return false;
            }

            return true;
        }

        public async void ShowLoadingView(string msg = "Please wait...", int closeAfter = 10)
        {
            _closeLoadingView = false;
            CheckShowingTimeOfLoading(closeAfter);

            await Task.Run(() =>
            {
                using (UserDialogs.Instance.Loading(msg, null, null, true, MaskType.Black))
                {
                    while (true)
                    {
                        if (_closeLoadingView)
                            break;
                    }
                }
            });
        }

        /// <summary>
        /// Close loading message window.
        /// </summary>
        public void CloseLoadingView()
        {
            _closeLoadingView = true;
        }

        /// <summary>
        /// Make sure the message loading window is no longer than '' seconds, then close it.
        /// </summary>
        private void CheckShowingTimeOfLoading(int seconds)
        {
            Device.StartTimer(TimeSpan.FromSeconds(seconds), () =>
            {
                if (!_closeLoadingView)
                {
                    CloseLoadingView();
                }

                return false; // True = Repeat again, False = Stop the timer
            });
        }

        public void ShowNotification(string desctirption, string title)
        {
            //var notification = new NotificationRequest
            //{
            //    BadgeNumber = 1,
            //    Description = desctirption,
            //    Title = title,
            //    ReturningData = "Dummy",
            //    NotificationId = 113,
            //    Schedule = new NotificationRequestSchedule() { NotifyTime = DateTime.Now.AddSeconds(3) }
            //};

            //NotificationCenter.Current.Show(notification);
        }

        public void Vibrate()
        {
            try
            {
                // Use default vibration length
                Vibration.Vibrate();

                // Or use specified time
                var duration = TimeSpan.FromMilliseconds(100);
                Vibration.Vibrate(duration);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        } 

        public string AppVersion
        {
            get
            {
                return VersionTracking.CurrentVersion;
            }
        }
    }
}
