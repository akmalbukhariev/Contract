using Contract.Control;
using Contract.iOS.Renderers;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(PdfWebView), typeof(CustomPdfWebViewRenderer))]

namespace Contract.iOS.Renderers
{
    public class CustomPdfWebViewRenderer : WkWebViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (NativeView != null && e.NewElement != null)
            {
                var webView = NativeView as UIWebView;

                if (webView == null)
                    return;

                webView.ScalesPageToFit = true;
            }
        }
    }
}