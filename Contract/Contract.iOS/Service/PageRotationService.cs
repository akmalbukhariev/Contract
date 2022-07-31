using Contract.Interfaces;
using Contract.iOS.Service;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PageRotationService))]
namespace Contract.iOS.Service
{
    public class PageRotationService : IRotationService
    {
        public void DisableRotation()
        {
            AppDelegate.AllowRotation = false;
        }

        public void EnableRotation()
        {
            AppDelegate.AllowRotation = true;
        }
    }
}