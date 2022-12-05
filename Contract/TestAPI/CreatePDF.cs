using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
                Col_No = "№",
                Col_Service = "Xizmat",
                Col_ServiceType = "Xizmat turi",
                Col_Number = "Soni,",
                Col_Piece = "dona",
                Col_ServicePrice = "Xizmat narxi",
                Col_ServicePriceForOne = "Bir donasi",
                Col_For = "uchun",
                Col_ServicePriceForAll = "Jami: ",
                Col_QQS = "QQS 15%",
                Col_Value_QQS = "qiymati (QQS",
                Col_With = "bilan)"
            };

            DrawTable(page, gr, tableCol);

            string filename = "Contract.pdf";
            pdf.Save(filename);
            Console.WriteLine("Done........");
            
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = filename;
            proc.Start();

            Environment.Exit(0);
        }

        private void DrawTable(PdfPage page, XGraphics gr, TableInfo tableCol)
        {
            var color = XPens.Black;
            int height = 50;
            XPoint topLeft = new XPoint(40, 100);
            XPoint topRight = new XPoint(page.Width - 20, topLeft.Y);
            XPoint bottomRight = new XPoint(page.Width - 20, topLeft.Y + height);
            XPoint bottomLeft = new XPoint(topLeft.X, topLeft.Y + height);

            gr.DrawLine(color, topLeft, topRight);
            gr.DrawLine(color, topRight, bottomRight);
            gr.DrawLine(color, topLeft, bottomLeft);
            gr.DrawLine(color, bottomLeft, bottomRight);

            var fontColumn = new XFont("Times New Roman", 9, XFontStyle.Bold);
            var colorColumnText = XBrushes.Black;

            int widthOfNo = 30;
            double X1 = topLeft.X + widthOfNo;
            gr.DrawLine(color, new XPoint(X1, topLeft.Y), 
                               new XPoint(X1, topLeft.Y + height));  //No

            gr.DrawString(tableCol.Col_No, fontColumn, colorColumnText, new XPoint(topLeft.X + (widthOfNo / 2) - 5, topLeft.Y + height / 2));

            int widthOfServiceType = 180;
            double X2 = X1 + widthOfServiceType;
            gr.DrawLine(color, new XPoint(X2, topLeft.Y), 
                               new XPoint(X2, topLeft.Y + height));  //Xizmat turi

            gr.DrawString(tableCol.Col_ServiceType, fontColumn, colorColumnText, new XPoint(X1 + (widthOfServiceType / 2) - 20, topLeft.Y + height / 2));

            int widthOfCountValue = 50;
            double X3 = X2 + widthOfCountValue;
            gr.DrawLine(color, new XPoint(X3, topLeft.Y), 
                               new XPoint(X3, topLeft.Y + height)); //Soni, dona

            gr.DrawString(tableCol.Col_Number, fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, topLeft.Y + (height / 2) - 5));
            gr.DrawString(tableCol.Col_Piece, fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, topLeft.Y + (height / 2) + 5));

            int widthOfServicePrice = 120;
            double X4 = X3 + widthOfServicePrice;
            gr.DrawLine(color, new XPoint(X4, topLeft.Y),
                               new XPoint(X4, topLeft.Y + height)); //Xizmat narxi 
            
            gr.DrawString(tableCol.Col_ServicePrice, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 20, topLeft.Y + (height / 2) - 12));

            int heightOfServicePrice = 20;
            gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightOfServicePrice),
                               new XPoint(X4, topLeft.Y + heightOfServicePrice)); //Inner Xizmat narxi

            gr.DrawString(tableCol.Col_ServicePriceForOne, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 50, topLeft.Y + (height / 2) + 7));
            gr.DrawString(tableCol.Col_For, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 40, topLeft.Y + (height / 2) + 17));

            double widthOfOnePiece = widthOfServicePrice / 2;
            gr.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightOfServicePrice),
                               new XPoint(X3 + widthOfOnePiece, topLeft.Y + height));

            gr.DrawString(tableCol.Col_ServicePriceForAll, fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, topLeft.Y + (height / 2) + 10));

            int widthOfQQS = 50;
            double X5 = X4 + widthOfQQS;
            gr.DrawLine(color, new XPoint(X5, topLeft.Y),
                               new XPoint(X5, topLeft.Y + height)); //QQS

            gr.DrawString(tableCol.Col_QQS, fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, topLeft.Y + (height / 2) + 5));

            double X6 = X5 + page.Width;
            gr.DrawLine(color, new XPoint(X6, topLeft.Y),
                               new XPoint(X6, topLeft.Y + height)); //Xizmat qiymati(QQS bilan)

            gr.DrawString(tableCol.Col_Service, fontColumn, colorColumnText, new XPoint(X5 + 40, topLeft.Y + (height / 2) - 10));
            gr.DrawString(tableCol.Col_Value_QQS, fontColumn, colorColumnText, new XPoint(X5 + 30, topLeft.Y + (height / 2) + 2 ));
            gr.DrawString(tableCol.Col_With, fontColumn, colorColumnText, new XPoint(X5 + 45, topLeft.Y + (height / 2) + 15));
            
            #region Add row
            int heightOfRow = 20;
            double leftYLine = topLeft.Y + height;

            int count = 8;
            for (int i = 1; i < count; i++)
            { 
                leftYLine += heightOfRow;
                gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topRight.X, leftYLine));

                if (i == count - 1)
                {
                    gr.DrawString(tableCol.Col_ServicePriceForAll, fontColumn, colorColumnText, new XPoint(topLeft.X + 80, leftYLine - 7));
                }
                else
                {
                    gr.DrawString($"{i}", fontColumn, colorColumnText, new XPoint(topLeft.X + 15, leftYLine - 7));
                }
            }

            gr.DrawLine(color, new XPoint(topLeft.X, topLeft.Y + height),
                               new XPoint(topLeft.X, leftYLine));                      //Left up to bottom
            gr.DrawLine(color, new XPoint(topRight.X, topLeft.Y + height),
                               new XPoint(topRight.X, leftYLine));                     //Right up to bottom

            gr.DrawLine(color, new XPoint(X1, topLeft.Y + height),
                               new XPoint(X1, leftYLine - heightOfRow));               //No up to bottom

            gr.DrawLine(color, new XPoint(X2, topLeft.Y + height),
                               new XPoint(X2, leftYLine - heightOfRow));               //Xizmat turi up to bottom

            gr.DrawLine(color, new XPoint(X3, topLeft.Y + height),
                               new XPoint(X3, leftYLine - heightOfRow));               //Soni, dona up to bottom

            gr.DrawLine(color, new XPoint(X4, topLeft.Y + height),
                               new XPoint(X4, leftYLine));                             //Xizmat narxi up to bottom
             
            gr.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + height),  
                               new XPoint(X3 + widthOfOnePiece, leftYLine));           //Inner xizmat narxi up to bottom

            gr.DrawLine(color, new XPoint(X5, topLeft.Y + height),
                               new XPoint(X5, leftYLine));                             //QQS up to bottom

            gr.DrawLine(color, new XPoint(X6, topLeft.Y + height),
                               new XPoint(X6, leftYLine));                             // Xizmat qiymati QQS bilan up to bottom

            #endregion
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
            public string Col_For { get; set; }
        }
    }
}
