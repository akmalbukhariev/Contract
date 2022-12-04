using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestAPI
{
    public class CreatePDF
    {
        //http://www.pdfsharp.net/wiki/PDFsharpSamples.ashx
        public void Run()
        {
            //GlobalFontSettings.FontResolver = new FontResolver();
            //
            //var document = new PdfDocument();
            //var page = document.AddPage();
            //
            //var gfx = XGraphics.FromPdfPage(page);
            //var font = new XFont("Arial", 20, XFontStyle.Bold);
            //
            //var textColor = XBrushes.Black;
            //var layout = new XRect(20, 20, page.Width, page.Height);
            //var format = XStringFormats.Center;
            //
            //gfx.DrawString("Hello World!", font, textColor, layout, format);
            //gfx.DrawLine(new XPen(XColors.Red, 2), new XPoint(10, 10), new XPoint(60, 60));
            //
            //document.Save("helloworld.pdf");

            CreateContract();
        }

        private void CreateContract()
        {
            GlobalFontSettings.FontResolver = new FontResolver();
            
            var pdf = new PdfDocument();
            var page = pdf.AddPage();
            var gr = XGraphics.FromPdfPage(page);
             
            var fontContract = new XFont("Times New Roman", 11, XFontStyle.Bold);
            var layout = new XRect(20, 20, page.Width, page.Height);

            gr.DrawString("SHARTNOMA № 00001", fontContract, XBrushes.Black, layout, XStringFormats.TopCenter);

            TableInfo tableCol = new TableInfo()
            {
                Col_No = "1",
                Col_Service = "Xizmat",
                Col_ServiceType = "Xizmat turi",
                Col_Number = "Soni,",
                Col_Piece = "dona",
                Col_ServicePrice = "Xizmat narxi",
                Col_ServicePriceForOne = "Bir donasi uchun",
                Col_ServicePriceForAll = "Jami",
                Col_QQS = "QQS 15%,",
                Col_Value_QQS = "qiymati (QQS",
                Col_With = "bilan)"
            };

            DrawTable(page, gr, tableCol);

            pdf.Save("Contract.pdf");
            Console.WriteLine("Done........");
        }

        private void DrawTable(PdfPage page, XGraphics gr, TableInfo tableCol)
        {
            var color = XPens.Black;
            int height = 50;
            XPoint topLeft = new XPoint(40, 150);
            XPoint topRight = new XPoint(page.Width - 20, topLeft.Y);
            XPoint bottomRight = new XPoint(page.Width - 20, topLeft.Y + height);
            XPoint bottomLeft = new XPoint(topLeft.X, topLeft.Y + height);

            gr.DrawLine(color, topLeft, topRight);
            gr.DrawLine(color, topRight, bottomRight);
            gr.DrawLine(color, topLeft, bottomLeft);
            gr.DrawLine(color, bottomLeft, bottomRight);

            int widthOfNo = 30;
            int widthOfServiceType = 180;
            double X1 = topLeft.X + widthOfNo;
            gr.DrawLine(color, new XPoint(X1, topLeft.Y), 
                               new XPoint(X1, topLeft.Y + height));  //No

            double X2 = X1 + widthOfServiceType;
            gr.DrawLine(color, new XPoint(X2, topLeft.Y), 
                               new XPoint(X2, topLeft.Y + height));  //Xizmat turi

            int widthOfCountValue = 50;
            double X3 = X2 + widthOfCountValue;
            gr.DrawLine(color, new XPoint(X3, topLeft.Y), 
                               new XPoint(X3, topLeft.Y + height)); //Soni, dona

            int widthOfServicePrice = 120;
            double X4 = X3 + widthOfServicePrice;
            gr.DrawLine(color, new XPoint(X4, topLeft.Y),
                               new XPoint(X4, topLeft.Y + height)); //Xizmat narxi 

            int heightOfServicePrice = 20;
            gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightOfServicePrice),
                               new XPoint(X4, topLeft.Y + heightOfServicePrice)); //Inner Xizmat narxi

            double widthOfOnePiece = widthOfServicePrice / 2;
            gr.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightOfServicePrice),
                               new XPoint(X3 + widthOfOnePiece, topLeft.Y + height));

            int widthOfQQS = 50;
            double X5 = X4 + widthOfQQS;
            gr.DrawLine(color, new XPoint(X5, topLeft.Y),
                               new XPoint(X5, topLeft.Y + height)); //QQS
             
            double X6 = X5 + page.Width;
            gr.DrawLine(color, new XPoint(X6, topLeft.Y),
                               new XPoint(X6, topLeft.Y + height)); //Xizmat qiymati(QQS bilan)
        }

        class TableInfo
        {
            public string Col_No { get; set; }
            public string Col_Service { get; set; }
            public string Col_ServiceType { get; set; }
            public string Col_Number { get; set; }
            public string Col_Piece { get; set; }
            public string Col_ServicePrice { get; set; }
            public string Col_ServicePriceForOne { get; set; }
            public string Col_ServicePriceForAll { get; set; }
            public string Col_QQS { get; set; }
            public string Col_Value_QQS { get; set; }
            public string Col_With { get; set; } 
        }
    }
}
