using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Contract.Control;
using Contract.Droid.Renderers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PdfWebView), typeof(CustomPdfWebViewRenderer))]

namespace Contract.Droid.Renderers
{
    public class CustomPdfWebViewRenderer : WebViewRenderer
    {
        PdfWebView customWebView;

        public CustomPdfWebViewRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                customWebView = Element as PdfWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.Settings.JavaScriptEnabled = true;
                if (customWebView.Uri != null)
                {
                    Control.LoadUrl(string.Format("https://drive.google.com/viewerng/viewer?embedded=true&url={0}", WebUtility.UrlEncode(customWebView.Uri)));
                }
            }
        }

        /*
                var customWebView = Element as PdfWebView;
                Control.Settings.AllowUniversalAccessFromFileURLs = true;
                Control.LoadUrl(string.Format("file:///android_asset/pdfjs/web/viewer.html?file={0}", string.Format("file:///android_asset/Content/{0}", WebUtility.UrlEncode(customWebView.Uri))));
         */

        //protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    base.OnElementPropertyChanged(sender, e);
        //    if (customWebView.Uri != null)
        //    {
        //        Control.LoadUrl(customWebView.Uri.ToString());
        //    }
        //}
    }
}