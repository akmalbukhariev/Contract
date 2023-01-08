using Contract.Model;
using Contract.TouchTracking;
using LibContract.HttpModels;
using LibContract.HttpResponse;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Contract.Pages.UnapprovedContracts
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageSign : IPage
    {
        Dictionary<long, SKPath> inProgressPaths = new Dictionary<long, SKPath>();
        List<SKPath> completedPaths = new List<SKPath>();
        
        SKSurface surface;

        SKPaint paint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Black,
            StrokeWidth = 10,
            StrokeCap = SKStrokeCap.Round,
            StrokeJoin = SKStrokeJoin.Round
        };

        public PageSign()
        {
            InitializeComponent();
        }

        void OnTouchEffectAction(object sender, TouchActionEventArgs args)
        {
            lbYourSign.IsVisible = false;
            switch (args.Type)
            {
                case TouchActionType.Pressed:
                    if (!inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = new SKPath();
                        path.MoveTo(ConvertToPixel(args.Location));
                        inProgressPaths.Add(args.Id, path);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Moved:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        SKPath path = inProgressPaths[args.Id];
                        path.LineTo(ConvertToPixel(args.Location));
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Released:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        completedPaths.Add(inProgressPaths[args.Id]);
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;

                case TouchActionType.Cancelled:
                    if (inProgressPaths.ContainsKey(args.Id))
                    {
                        inProgressPaths.Remove(args.Id);
                        canvasView.InvalidateSurface();
                    }
                    break;
            }
        }

        void OnCanvasViewPaintSurface(object sender, SKPaintSurfaceEventArgs args)
        {
            surface = args.Surface;
            SKCanvas canvas = surface.Canvas;
            canvas.Clear();

            foreach (SKPath path in completedPaths)
            { 
                canvas.DrawPath(path, paint);
            }

            foreach (SKPath path in inProgressPaths.Values)
            {
                canvas.DrawPath(path, paint);
            }
        }

        SKPoint ConvertToPixel(Point pt)
        {
            return new SKPoint((float)(canvasView.CanvasSize.Width * pt.X / canvasView.Width),
                               (float)(canvasView.CanvasSize.Height * pt.Y / canvasView.Height));
        }

        private async void Delete_Tapped(object sender, EventArgs e)
        {
            ClickAnimationView((Image)sender);
            bool res = await Application.Current.MainPage.DisplayAlert(RSC.Clean, RSC.CleanMessage, RSC.Ok, RSC.Cancel);
            if (res)
            {
                completedPaths.Clear();
                inProgressPaths.Clear();
                canvasView.InvalidateSurface();
                lbYourSign.IsVisible = true;
            }
        }

        private async void Save_Clicked(object sender, EventArgs e)
        {
            //var mainDir = FileSystem.AppDataDirectory;
            //var strFilePath = Path.Combine(mainDir, $"12_{DateTime.Now.ToString("yyyyMMdd_hhmmss.fff")}_.png");

            float x = float.MaxValue;
            float y = float.MaxValue;
            float width = 0.0f;
            float height = 0.0f;
            CalcSize(ref x, ref y, ref width, ref height);

            SKBitmap signBitmap = new SKBitmap((int)(width - x) + 20, (int)(height - y) + 20);
            SKCanvas canvas = new SKCanvas(signBitmap);
             
            x -= 15;
            y -= 15;
            
            foreach (SKPath path in completedPaths)
            {
                SKPath temp = new SKPath();
                temp.MoveTo(new SKPoint(path.Points[0].X - x, path.Points[0].Y - y));

                for (int i = 1; i < path.Points.Length; i++)
                {
                    temp.LineTo(new SKPoint(path.Points[i].X - x, path.Points[i].Y - y));
                }
                canvas.DrawPath(temp, paint);
            }
             
            SKImage image = SKImage.FromBitmap(signBitmap);
            SKData data = image.Encode();

            SignatureData info = new SignatureData();
            info.phone_number = ControlApp.UserInfo.phone_number;
            info.dataStream = data.AsStream();

            ControlApp.ShowLoadingView(RSC.PleaseWait);
            ResponseSignatureInfo response = await Net.HttpService.SetSignature(info);
            ControlApp.CloseLoadingView();

            string strMessage = response.result ? RSC.SuccessfullyCompleted : RSC.Failed;
            await DisplayAlert(RSC.SignWindow, strMessage, RSC.Ok);
            await Navigation.PopAsync();
        }

        void CalcSize(ref float x, ref float y, ref float width, ref float height)
        {
            foreach (SKPath path in completedPaths)
            {
                float maxX = path.Points.Max(item => item.X);
                float maxY = path.Points.Max(item => item.Y);

                float minX = path.Points.Min(item => item.X);
                float minY = path.Points.Min(item => item.Y);

                if (width <= maxX) width = maxX;
                if (height <= maxY) height = maxY;

                if (x >= minX) x = minX;
                if (y >= minY) y = minY;
            }
        }
    }
}