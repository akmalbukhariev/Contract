using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
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
            var fontText = new XFont("Times New Roman", 11, XFontStyle.Regular);

            //var layout1 = new XRect(20, 20, page.Width, page.Height);
            //XSize ss = gr.MeasureString(Constans.str1, fontText);
            // 
            //gr.DrawString(Constans.str1, fontText, XBrushes.Black, layout1, XStringFormats.TopCenter);
            //
            //var layout2 = new XRect(20, 40, page.Width, page.Height);
            //gr.DrawString(Constans.str2, fontText, XBrushes.Black, layout2, XStringFormats.TopLeft);
            //
            //var layout3 = new XRect(20, 70, page.Width, page.Height);
            //gr.DrawString(Constans.str3, fontText, XBrushes.Black, layout3, XStringFormats.TopCenter);
            //
            //var layout4 = new XRect(20, 100, page.Width, page.Height);
            //gr.DrawString(Constans.str4, fontText, XBrushes.Black, layout4, XStringFormats.TopLeft);


            XTextFormatter tf = new XTextFormatter(gr);
            tf.DrawString(Constans.str2, fontText, XBrushes.Black, new XRect(20, 250, page.Width - 10, page.Height - 10));


            //const XFontStyle style = XFontStyle.Regular; 
            //XFont font = new XFont("Times New Roman", 20, style);  
            //const string text = Constans.str4; 
            //const double x = 20, y = 230; 
            //XSize size = gr.MeasureString(text, font);  
            //double lineSpace = font.GetHeight(); 
            //int cellSpace = font.FontFamily.GetLineSpacing(style); 
            //int cellAscent = font.FontFamily.GetCellAscent(style); 
            //int cellDescent = font.FontFamily.GetCellDescent(style); 
            //int cellLeading = cellSpace - cellAscent - cellDescent;  
            //double ascent = lineSpace * cellAscent / cellSpace;
            //gr.DrawRectangle(XBrushes.Bisque, x, y - ascent, size.Width, ascent);  
            //double descent = lineSpace * cellDescent / cellSpace;
            //gr.DrawRectangle(XBrushes.LightGreen, x, y, size.Width, descent);  
            //double leading = lineSpace * cellLeading / cellSpace;
            //gr.DrawRectangle(XBrushes.Yellow, x, y + descent, size.Width, leading);  
            //XColor color = XColors.DarkSlateBlue; 
            //color.A = 0.6;
            //gr.DrawString(text, font, new XSolidBrush(color), x, y);




            TableColumn1 tableCol = new TableColumn1()
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
            List<TableRow1> rows = new List<TableRow1>();
            TableRow1 row1 = new TableRow1()
            {
                ServiceType = "Ruchka",
                Count = "3",
                PriceForOne = "123",
                PriceForAll = "369",
                QQS = "15%",
                PriceWithQQS = "50000"
            };
            TableRow1 row2 = new TableRow1()
            {
                ServiceType = "Rangli qalam",
                Count = "1",
                PriceForOne = "300",
                PriceForAll = "45",
                QQS = "15%",
                PriceWithQQS = "45000"
            };
            TableRow1 row3 = new TableRow1()
            {
                ServiceType = "Plamaster",
                Count = "9",
                PriceForOne = "750",
                PriceForAll = "456",
                QQS = "15%",
                PriceWithQQS = "15420"
            };
            TableRow1 row4 = new TableRow1()
            {
                ServiceType = "Kitob",
                Count = "1",
                PriceForOne = "300",
                PriceForAll = "45",
                QQS = "15%",
                PriceWithQQS = "45000"
            };
            TableRow1 row5 = new TableRow1()
            {
                ServiceType = "Daftar",
                Count = "45",
                PriceForOne = "8500",
                PriceForAll = "9543",
                QQS = "15%",
                PriceWithQQS = "79025"
            };
            rows.Add(row1);
            rows.Add(row2);
            rows.Add(row3);
            rows.Add(row4);
            rows.Add(row5);

            //DrawTable1(page, gr, tableCol, rows);

            SellerBuyer rowInfo = new SellerBuyer()
            {
                Companyname = "",
                Address = "Manzil",
                AccountNumber = "Hisob raqam",
                BankCode = "Bank kodi",
                Stir = "Stir",
                Oked = "OKED",
                Phone = "Telefon",
                Mu = "M.U",
                Sign = "(imzo)"
            };
            SellerBuyer seller = new SellerBuyer()
            {
                SellerOrBuyer = "Bajaruvchi",
                Companyname = "Company 1",
                Address = "Manzil: WWWWWWWWWW",
                AccountNumber = "Xisob raqami: 000123541",
                BankName = "ASAKA",
                BankCode = "Bank kodi: 0000321",
                Stir = "STIR: 1234568",
                Oked = "OKED: 222222",
                Phone = "Telefon: +998925478",
                FIO = "Direktor",
                Mu = "M.U",
                Sign = "(imzo)"
            };
            SellerBuyer buyer = new SellerBuyer()
            {
                SellerOrBuyer = "Buyurtmachi",
                Companyname = "Company 2",
                Address = "Manzil: XXXXXXXX",
                AccountNumber = "Xisob raqam: 8521452",
                BankName = "TURON",
                BankCode = "Bank kodi: 9632541",
                Stir = "STIR: 987541",
                Oked = "OKED: 888874",
                Phone = "Telefon: +998956321",
                FIO = "Direktor",
                Mu = "M.U",
                Sign = "(imzo)"
            };

            //DrawTable2(page, gr, rowInfo, seller, buyer);

            string filename = "Contract.pdf";
            pdf.Save(filename);
            Console.WriteLine("Done........");
            
            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = filename;
            proc.Start();

            Environment.Exit(0);
        }

        private void DrawTable1(PdfPage page, XGraphics gr, TableColumn1 tableCol, List<TableRow1> rows)
        {
            var color = XPens.Black;
            int height = 50;
            XPoint topLeft = new XPoint(40, 50);
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
             
            for (int i = 0; i < rows.Count; i++)
            { 
                leftYLine += heightOfRow;
                gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topRight.X, leftYLine));

                if (i == rows.Count - 1)
                {
                    gr.DrawString(tableCol.Col_ServicePriceForAll, fontColumn, colorColumnText, new XPoint(topLeft.X + 80, leftYLine - 7));
                    gr.DrawString("500,00", fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, leftYLine - 7));
                    gr.DrawString("250,000", fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, leftYLine - 7));
                    gr.DrawString("1000,00", fontColumn, colorColumnText, new XPoint(X5 + 45, leftYLine - 7));
                }
                else
                {
                    int No = i + 1;
                    gr.DrawString($"{No}", fontColumn, colorColumnText, new XPoint(topLeft.X + 15, leftYLine - 7));
                    gr.DrawString($"{rows[i].ServiceType}", fontColumn, colorColumnText, new XPoint(X1 + 10, leftYLine - 7));
                    gr.DrawString($"{rows[i].Count}", fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, leftYLine - 7));
                    gr.DrawString($"{rows[i].PriceForOne}", fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 50, leftYLine - 7));
                    gr.DrawString($"{rows[i].PriceForAll}", fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, leftYLine - 7));
                    gr.DrawString($"{rows[i].QQS}", fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, leftYLine - 7));
                    gr.DrawString($"{rows[i].PriceWithQQS}", fontColumn, colorColumnText, new XPoint(X5 + 45, leftYLine - 7));
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

        private void DrawTable2(PdfPage page, XGraphics gr, SellerBuyer rowInfo, SellerBuyer seller, SellerBuyer buyer)
        {
            var color = XPens.Black;
            var font1 = new XFont("Times New Roman", 11, XFontStyle.Bold);
            var font2 = new XFont("Times New Roman", 9, XFontStyle.Bold);
            int height = 25;
            int innerHeight = 20;

            XPoint topLeft = new XPoint(40, 270);
            XPoint topRight = new XPoint(page.Width - 20, topLeft.Y);

            int wM = 15;
            double mX = (topLeft.X + topRight.X) / 2;

            gr.DrawLine(color, topLeft, topRight); 
            gr.DrawString(seller.SellerOrBuyer, font1, XBrushes.Black, new XPoint(topLeft.X + 100, topLeft.Y + 15));
            gr.DrawString(buyer.SellerOrBuyer, font1, XBrushes.Black, new XPoint(mX + 110, topLeft.Y + 15));

            double Y1 = topLeft.Y + height;
            gr.DrawLine(color, new XPoint(topLeft.X, Y1), new XPoint(topRight.X, Y1));
            gr.DrawString(seller.Companyname, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y1 + 15));
            gr.DrawString(buyer.Companyname, font2, XBrushes.Black, new XPoint(mX + 20, Y1 + 15));

            double Y2 = Y1 + height;
            gr.DrawLine(color, new XPoint(topLeft.X, Y2), new XPoint(topRight.X, Y2));
            gr.DrawString(seller.Address, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y2 + 15));
            gr.DrawString(buyer.Address, font2, XBrushes.Black, new XPoint(mX + 20, Y2 + 15));

            double Y3 = Y2 + height;
            gr.DrawLine(color, new XPoint(topLeft.X, Y3), new XPoint(topRight.X, Y3));
            gr.DrawString(seller.AccountNumber, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y3 + 15));
            gr.DrawString(buyer.AccountNumber, font2, XBrushes.Black, new XPoint(mX + 20, Y3 + 15));

            double Y4 = Y3 + innerHeight;
            gr.DrawLine(color, new XPoint(topLeft.X, Y4), new XPoint(topRight.X, Y4));
            gr.DrawString(seller.BankName, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y4 + 15));
            gr.DrawString(buyer.BankName, font2, XBrushes.Black, new XPoint(mX + 20, Y4 + 15));

            double Y5 = Y4 + innerHeight;
            gr.DrawLine(color, new XPoint(topLeft.X, Y5), new XPoint(topRight.X, Y5));
            gr.DrawString(seller.BankCode, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y5 + 15));
            gr.DrawString(buyer.BankCode, font2, XBrushes.Black, new XPoint(mX + 20, Y5 + 15));

            double Y6 = Y5 + innerHeight;
            gr.DrawLine(color, new XPoint(topLeft.X, Y6), new XPoint(topRight.X, Y6));
            gr.DrawString(seller.Stir, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y6 + 15));
            gr.DrawString(buyer.Stir, font2, XBrushes.Black, new XPoint(mX + 20, Y6 + 15));

            double Y7 = Y6 + innerHeight;
            gr.DrawLine(color, new XPoint(topLeft.X, Y7), new XPoint(topRight.X, Y7));
            gr.DrawString(seller.Oked, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y7 + 15));
            gr.DrawString(buyer.Oked, font2, XBrushes.Black, new XPoint(mX + 20, Y7 + 15));

            double Y8 = Y7 + innerHeight;
            gr.DrawLine(color, new XPoint(topLeft.X, Y8), new XPoint(topRight.X, Y8));
            gr.DrawString(seller.Phone, font2, XBrushes.Black, new XPoint(topLeft.X + 10, Y8 + 15));
            gr.DrawString(buyer.Phone, font2, XBrushes.Black, new XPoint(mX + 20, Y8 + 15));
             

            double Y9 = Y8 + height;
            gr.DrawLine(color, new XPoint(topLeft.X, Y9), new XPoint(topRight.X, Y9));

            double Y10 = Y9 + innerHeight;
            gr.DrawLine(color, new XPoint(topLeft.X, Y10), new XPoint(topRight.X, Y10));

            double Y11 = Y10 + height;
            gr.DrawLine(color, new XPoint(topLeft.X, Y11), new XPoint(topRight.X, Y11));

            double wMu = 30;
            gr.DrawLine(color, new XPoint(topLeft.X + wMu, Y10), new XPoint(topLeft.X + wMu, Y11)); //M.U Left line
            gr.DrawLine(color, new XPoint(mX + wM + wMu, Y10), new XPoint(mX + wM + wMu, Y11));     //M.U Right line

            gr.DrawString(seller.Mu, font2, XBrushes.Black, new XPoint(topLeft.X + 5, Y11 - 5));  //M.U Left
            gr.DrawString(seller.Mu, font2, XBrushes.Black, new XPoint(mX + 20, Y11 - 5));        //M.U Left

            gr.DrawString(seller.Sign, font2, XBrushes.Black, new XPoint(mX - 80, Y11 + 8));         //Sign Left
            gr.DrawString(seller.Sign, font2, XBrushes.Black, new XPoint(topRight.X - 80, Y11 + 8)); //Sign Right

            double Y12 = Y11 + height;
            gr.DrawLine(color, new XPoint(topLeft.X, Y12), new XPoint(topRight.X, Y12));
             
            gr.DrawLine(color, topLeft, new XPoint(topLeft.X, Y12));   //Left up to bottom 
            gr.DrawLine(color, new XPoint(mX - wM, topLeft.Y), new XPoint(mX - wM, Y12)); //Left middle up to bottom
            gr.DrawLine(color, new XPoint(mX + wM, topLeft.Y), new XPoint(mX + wM, Y12)); //Right middle up to bottom 
            gr.DrawLine(color, topRight, new XPoint(topRight.X, Y12)); //Right up to bottom
        }

        class TableColumn1
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

        class TableRow1
        {
            /// <summary>
            /// Hizmat turi
            /// </summary>
            public string ServiceType { get; set; } = "";
            /// <summary>
            /// Soni, dona
            /// </summary>
            public string Count { get; set; } = "";
            /// <summary>
            /// Bir donasi uchun
            /// </summary>
            public string PriceForOne { get; set; } = "";
            /// <summary>
            /// Jami
            /// </summary>
            public string PriceForAll { get; set; } = "";
            public string QQS { get; set; } = "";
            /// <summary>
            /// Hizmat qiymati(QQS bilan)
            /// </summary>
            public string PriceWithQQS { get; set; } = "";
        }

        class SellerBuyer
        {
            public string SellerOrBuyer { get; set; }
            public string Companyname { get; set; }
            public string Address { get; set; }
            public string AccountNumber { get; set; }
            public string BankName { get; set; }
            public string BankCode { get; set; }
            public string Stir { get; set; }
            public string Oked { get; set; }
            public string Phone { get; set; }
            public string Sign { get; set; }
            public string Mu { get; set; }
            public string FIO { get; set; }
        }
    }
}
