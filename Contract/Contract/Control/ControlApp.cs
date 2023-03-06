using Acr.UserDialogs;
using LibContract.HttpModels;
using Contract.Model;
using System;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Plugin.LocalNotification;
using System.Linq;
using System.IO;

namespace Contract.Control
{ 
    public class ControlApp
    {
        public delegate void CurrencyCostChanged();
        public event CurrencyCostChanged EventCurrencyCostChanged;

        private bool _closeLoadingView = false;
        public bool UnapprovedPageClosed = false;
        public bool AppStarting { get; set; }
        public bool AppOnResume { get; set; }
        public bool AppOnSleep { get; set; }
        /// <summary>
        /// Remove the item If canceled had been successfully
        /// </summary>
        public bool CanIRemove { get; set; } = false;
        //public string UserId { get; set; }
        public string LanguageId = "";

        private static ControlApp _instance = null;

        //public ContractNumberTemplate SelectedContractNumberFormat { get; set; } = null;
        public CompanyInfo SelectedClientCompanyInfo { get; set; } = null;
        public CompanyInfo UserCompanyInfo { get; set; } = null;
        public EditTemplate SelectedEditTemplate { get; set; }
        public Login LoginInfo { get; set; } = new Login();
        public User UserInfo { get; set; }
        public bool OpenClientInfo { get; set; }
        public bool OpenSearchClient { get; set; }
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
                UserDialogs.Instance.Alert(RSC.NoInternet, RSC.Internet, RSC.Ok);
                return false;
            }

            return true;
        }

        public bool CheckResponse(object response)
        {
            if (response == null)
            {
                UserDialogs.Instance.Alert(RSC.ServerMessage1, RSC.Error, RSC.Ok);
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

        public void ChangedCurrentPrice()
        {
            EventCurrencyCostChanged?.Invoke();
        }

        public string GetDateFromStr(string strDate)
        {
            DateTime date = DateTime.ParseExact(strDate, "yyyyMMdd_hhmmss.fff", null);
            return date.ToString("yyyy-MM-dd hh:mm:ss");
        }

        public async Task ShowNotification(string desctirption, string title)
        {
            //https://github.com/thudugala/Plugin.LocalNotification/blob/master/Sample/Direct/LocalNotification.Sample/MainPage.xaml.cs
            //https://www.nuget.org/packages/Plugin.LocalNotification/9.0.1

            var imageStream = GetType().Assembly.GetManifestResourceStream("Contract.ic_launcher.png");
            byte[] imageBytes = null;

            if (imageStream != null)
            {
                using (var ms = new MemoryStream())
                {
                    await imageStream.CopyToAsync(ms);
                    imageBytes = ms.ToArray();
                }
            }

            //NotificationImage image = new NotificationImage();
            //image.FilePath = "ic_launcher";

            var notification = new NotificationRequest
            {
                BadgeNumber = 1,
                Description = desctirption,
                Title = title,
                ReturningData = "Dummy",
                NotificationId = 113,
                
                Android =
                {
                    IconSmallName =
                    {
                        ResourceName = "ic_launcher",
                    },
                    IconLargeName =
                    {
                        ResourceName = "ic_launcher",
                    },
                    Priority = Plugin.LocalNotification.AndroidOption.AndroidNotificationPriority.Max,
                    AutoCancel = true,
                    Ongoing = true,
                    ChannelId = "General"
                    //Color =
                    //{
                    //    ResourceName = "colorPrimary"
                    //} 
                },
                iOS =
                {
                    HideForegroundAlert = true,
                    PlayForegroundSound = true
                },

                Schedule = new NotificationRequestSchedule() { NotifyTime = DateTime.Now.AddSeconds(3) }
            };
             
            await NotificationCenter.Current.Show(notification);
        }

        public async Task ShareUri(string uri)
        {
            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = uri,
                Title = RSC.SendContract
            });
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
