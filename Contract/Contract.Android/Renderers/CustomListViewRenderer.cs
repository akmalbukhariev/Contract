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
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomListView), typeof(CustomListViewRenderer))]

namespace Contract.Droid.Renderers
{
    public class CustomListViewRenderer : ListViewRenderer
    {
        public CustomListViewRenderer()
        {
            this.LongClick += CustomListViewRenderer_LongClick; 
        }

        private void CustomListViewRenderer_LongClick(object sender, LongClickEventArgs e)
        {
            int i = 0;
        }
    }
}