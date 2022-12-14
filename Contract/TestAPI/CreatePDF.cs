using LibContract.HttpModels;
using Newtonsoft.Json;
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
        //http://developer.th-soft.com/developer/2015/07/17/pdfsharp-improving-the-xtextformatter-class-measuring-the-height-of-the-text/
        public void Run()
        { 
            //CreateContract();
            CreateNormalContract();
        }

        private void CreateContract()
        {
            GlobalFontSettings.FontResolver = new FontResolver();
            
            var pdf = new PdfDocument(); 

            TableColumn tableCol = new TableColumn()
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
            List<TableRow> rows = new List<TableRow>();
            TableRow row1 = new TableRow()
            {
                ServiceType = "Ruchka",
                Count = "3",
                PriceForOne = "123",
                PriceForAll = "369",
                QQS = "15%",
                PriceWithQQS = "50000"
            };
            TableRow row2 = new TableRow()
            {
                ServiceType = "Rangli qalam",
                Count = "1",
                PriceForOne = "300",
                PriceForAll = "45",
                QQS = "15%",
                PriceWithQQS = "45000"
            };
            TableRow row3 = new TableRow()
            {
                ServiceType = "Plamaster",
                Count = "9",
                PriceForOne = "750",
                PriceForAll = "456",
                QQS = "15%",
                PriceWithQQS = "15420"
            };
            TableRow row4 = new TableRow()
            {
                ServiceType = "Kitob",
                Count = "1",
                PriceForOne = "300",
                PriceForAll = "45",
                QQS = "15%",
                PriceWithQQS = "45000"
            };
            TableRow row5 = new TableRow()
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

            double lastYOfTable = 0.0;
            DrawTable1(pdf, null, null, tableCol, rows, 600, ref lastYOfTable);

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

        private void CreateNormalContract()
        {
            GlobalFontSettings.FontResolver = new FontResolver();
            var pdf = new PdfDocument();
             
            CreateContractInfo crtInfo = new CreateContractInfo();
            crtInfo.amount_of_qqs = "10";

            double qqs = double.Parse(crtInfo.amount_of_qqs.Replace("%","").Trim());
            List<ServicesInfo> serviceList = new List<ServicesInfo>();
            ServicesInfo item1 = new ServicesInfo()
            {
                name_of_service = "Service 1",
                amount_value = 12,
                amount_value_price = "10"
            };
            ServicesInfo item2 = new ServicesInfo()
            {
                name_of_service = "Service 2",
                amount_value = 3,
                amount_value_price = "28"
            };
            ServicesInfo item3 = new ServicesInfo()
            {
                name_of_service = "Service 3",
                amount_value = 20,
                amount_value_price = "50"
            };
            ServicesInfo item4 = new ServicesInfo()
            {
                name_of_service = "Service 4",
                amount_value = 6,
                amount_value_price = "4"
            };
            serviceList.Add(item1);
            serviceList.Add(item2);
            serviceList.Add(item3);
            //serviceList.Add(item4);

            TableColumn tableCols = new TableColumn()
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
                Col_QQS = $"QQS {qqs}%",
                Col_Value_QQS = "qiymati (QQS",
                Col_With = "bilan)"
            };

            List<TableRow> tableRows = new List<TableRow>();
            foreach (ServicesInfo item in serviceList)
            {
                tableRows.Add(new TableRow(item, qqs));
            }

            List<ContractTemplateJson> jsonList = JsonConvert.DeserializeObject<List<ContractTemplateJson>>(CreateClauses.Popular());
            CreatePage(jsonList, 0, 0, pdf, tableCols, tableRows);

            string filename = "Contract.pdf";
            pdf.Save(filename);
            Console.WriteLine("Done........");

            Process proc = new Process();
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.FileName = filename;
            proc.Start();

            Environment.Exit(0);
        }

        private void CreatePage(List<ContractTemplateJson> jsonList, int index, int chIndex, PdfDocument pdf, TableColumn tableCols = null, List<TableRow> tableRows = null, bool drawedTitle = false)
        {
            var page = pdf.AddPage();
            var gr = XGraphics.FromPdfPage(page);

            var fontTitle = new XFont("Times New Roman", 11, XFontStyle.Bold);
            var fontText = new XFont("Times New Roman", 11, XFontStyle.Regular);

            XRect layoutText = new XRect(20, 50, page.Width - 20, page.Height - 20);
            XRect layoutNextText = layoutText;

            XSolidBrush textColor = XBrushes.Black;
            XTextFormatterEx calcHegihtOfText = new XTextFormatterEx(gr);
            XTextFormatter drawText = new XTextFormatter(gr);

            double lineHeight = 12.181640625;

            for (int i = index; i < jsonList.Count; i++)
            {
                double lastYOfTable = layoutNextText.Y;

                ContractTemplateJson item = jsonList[i];
                if (item.IsContractServiceDetailsButton)
                {
                    if (layoutNextText.Y >= page.Height - lineHeight * 4)
                    {
                        DrawTable1(pdf, null, gr, tableCols, tableRows, layoutNextText.Y, ref lastYOfTable);
                    }
                    else
                    {
                        DrawTable1(pdf, page, gr, tableCols, tableRows, layoutNextText.Y, ref lastYOfTable);
                    }
                    layoutNextText.Y += lastYOfTable;
                }
                else if (item.IsContractInfoButton)
                {

                }
                else
                {    
                    if (chIndex == 0 && !drawedTitle)
                    {
                        string strTitle = $"{item.Title}. {item.Description}";
                        double textHeight = calcHegihtOfText.ClacHeightOfText(fontText, layoutNextText, strTitle);
                        double lastY = layoutNextText.Y + textHeight;

                        if (lastY >= page.Height - lineHeight * 4)
                        {
                            CreatePage(jsonList, i, 0, pdf);
                            break;
                        }
                        
                        drawText.Alignment = XParagraphAlignment.Center;
                        drawText.DrawString(strTitle, fontTitle, textColor, layoutNextText);

                        layoutNextText.Y += textHeight;
                    }

                    for (int j = chIndex; j < item.Child.Count; j++)
                    { 
                        ContractTemplateJson chItem = item.Child[j];
                        string strChild = $"{chItem.Title}  {chItem.Description}";
                        double textHeight = calcHegihtOfText.ClacHeightOfText(fontText, layoutNextText, strChild);
                        double lastY = layoutNextText.Y + textHeight;

                        if (lastY >= page.Height - lineHeight * 4)
                        {
                            if (j == 0)
                                CreatePage(jsonList, i, j, pdf, tableCols, tableRows, true);
                            else
                                CreatePage(jsonList, i, j, pdf);
                            return;
                        }

                        drawText.Alignment = XParagraphAlignment.Left;
                        drawText.DrawString(strChild, fontText, textColor, layoutNextText);

                        layoutNextText.Y += textHeight;
                    }

                    chIndex = 0;
                    drawedTitle = false;
                    layoutNextText.Y += lineHeight * 2;
                }
            }
        }

        private void DrawTable1(PdfDocument pdf, PdfPage oldPage, XGraphics oldGr, TableColumn tableCols, List<TableRow> tableRows, double startY, ref double endYOfTable, int rowIndex = 0, bool drawedHeader = false)
        {
            var page = oldPage == null? pdf.AddPage() : oldPage;
            var gr = oldPage == null ? XGraphics.FromPdfPage(page) : oldGr;

            double lineHeight = 12.181640625;

            endYOfTable = startY;
            if (startY >= page.Height - lineHeight * 4)
            {
                double lastY = 0.0;
                DrawTable1(pdf, null, gr, tableCols, tableRows, 20, ref lastY);
                return;
            }

            var color = XPens.Black;
            int heightHeader = drawedHeader? 0 : 50;

            XPoint topLeft = new XPoint(20, startY);
            XPoint topRight = new XPoint(page.Width - 20, topLeft.Y);
            XPoint bottomRight = new XPoint(page.Width - 20, topLeft.Y + heightHeader);
            XPoint bottomLeft = new XPoint(topLeft.X, topLeft.Y + heightHeader);

            gr.DrawLine(color, topLeft, topRight);

            if (!drawedHeader)
            {
                gr.DrawLine(color, topRight, bottomRight);
                gr.DrawLine(color, topLeft, bottomLeft);
                gr.DrawLine(color, bottomLeft, bottomRight);
            }

            #region Column header
            var fontColumn = new XFont("Times New Roman", 9, XFontStyle.Bold);
            var colorColumnText = XBrushes.Black;

            int widthOfNo = 30;
            double X1 = topLeft.X + widthOfNo;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X1, topLeft.Y), 
                                   new XPoint(X1, topLeft.Y + heightHeader));  //No

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_No, fontColumn, colorColumnText, new XPoint(topLeft.X + (widthOfNo / 2) - 5, topLeft.Y + heightHeader / 2));

            int widthOfServiceType = 180;
            double X2 = X1 + widthOfServiceType;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X2, topLeft.Y), 
                                   new XPoint(X2, topLeft.Y + heightHeader));  //Xizmat turi

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_ServiceType, fontColumn, colorColumnText, new XPoint(X1 + (widthOfServiceType / 2) - 20, topLeft.Y + heightHeader / 2));

            int widthOfCountValue = 50;
            double X3 = X2 + widthOfCountValue;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X3, topLeft.Y), 
                                   new XPoint(X3, topLeft.Y + heightHeader)); //Soni, dona

            if (!drawedHeader)
            {
                gr.DrawString(tableCols.Col_Number, fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, topLeft.Y + (heightHeader / 2) - 5));
                gr.DrawString(tableCols.Col_Piece, fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, topLeft.Y + (heightHeader / 2) + 5));
            }

            int widthOfServicePrice = 120;
            double X4 = X3 + widthOfServicePrice;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X4, topLeft.Y),
                                   new XPoint(X4, topLeft.Y + heightHeader)); //Xizmat narxi 

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_ServicePrice, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 20, topLeft.Y + (heightHeader / 2) - 12));

            int heightOfServicePrice = 20;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightOfServicePrice),
                                   new XPoint(X4, topLeft.Y + heightOfServicePrice)); //Inner Xizmat narxi

            if (!drawedHeader)
            {
                gr.DrawString(tableCols.Col_ServicePriceForOne, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 50, topLeft.Y + (heightHeader / 2) + 7));
                gr.DrawString(tableCols.Col_For, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 40, topLeft.Y + (heightHeader / 2) + 17));
            }

            double widthOfOnePiece = widthOfServicePrice / 2;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightOfServicePrice),
                                   new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightHeader));

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_ServicePriceForAll, fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, topLeft.Y + (heightHeader / 2) + 10));

            int widthOfQQS = 50;
            double X5 = X4 + widthOfQQS;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X5, topLeft.Y),
                                   new XPoint(X5, topLeft.Y + heightHeader)); //QQS

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_QQS, fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, topLeft.Y + (heightHeader / 2) + 5));

            double X6 = X5 + page.Width;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X6, topLeft.Y),
                                   new XPoint(X6, topLeft.Y + heightHeader)); //Xizmat qiymati(QQS bilan)

            if (!drawedHeader)
            {
                gr.DrawString(tableCols.Col_Service, fontColumn, colorColumnText, new XPoint(X5 + 40, topLeft.Y + (heightHeader / 2) - 10));
                gr.DrawString(tableCols.Col_Value_QQS, fontColumn, colorColumnText, new XPoint(X5 + 30, topLeft.Y + (heightHeader / 2) + 2));
                gr.DrawString(tableCols.Col_With, fontColumn, colorColumnText, new XPoint(X5 + 45, topLeft.Y + (heightHeader / 2) + 15));
            } 
            #endregion

            #region Add row
            int heightOfRow = 20;
            double leftYLine = topLeft.Y + heightHeader;

            double priceForAll = 0.0;
            double qqs = 0.0;
            double priceWithQQS = 0.0;
            foreach (TableRow row in tableRows)
            {
                priceForAll += double.Parse(row.PriceForAll);
                qqs += double.Parse(row.QQS.Replace("%",""));
                priceWithQQS += double.Parse(row.PriceWithQQS);
            }

            for (int i = rowIndex; i < tableRows.Count; i++)
            {
                double lastY = leftYLine + heightOfRow;

                if (lastY >= page.Height - lineHeight * 4)
                {
                    DrawTable1(pdf, null, gr, tableCols, tableRows, 20, ref lastY, i, true);

                    gr.DrawLine(color, new XPoint(X1, topLeft.Y + heightHeader), new XPoint(X1, leftYLine));    //No up to bottom
                    gr.DrawLine(color, new XPoint(X2, topLeft.Y + heightHeader), new XPoint(X2, leftYLine));    //Xizmat turi up to bottom
                    gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightHeader), new XPoint(X3, leftYLine));    //Soni, dona up to bottom
                    break;
                }

                leftYLine += heightOfRow;
                gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topRight.X, leftYLine));
                 
                int No = i + 1;
                gr.DrawString($"{No}", fontColumn, colorColumnText, new XPoint(topLeft.X + 15, leftYLine - 7));
                gr.DrawString($"{tableRows[i].ServiceType}", fontColumn, colorColumnText, new XPoint(X1 + 10, leftYLine - 7));
                gr.DrawString($"{tableRows[i].Count}", fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, leftYLine - 7));
                gr.DrawString($"{tableRows[i].PriceForOne}", fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 50, leftYLine - 7));
                gr.DrawString($"{tableRows[i].PriceForAll}", fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, leftYLine - 7));
                gr.DrawString($"{tableRows[i].QQS}", fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, leftYLine - 7));
                gr.DrawString($"{tableRows[i].PriceWithQQS}", fontColumn, colorColumnText, new XPoint(X5 + 45, leftYLine - 7));

                if (i == tableRows.Count - 1)
                {
                    leftYLine += heightOfRow;
                    gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topRight.X, leftYLine));
                    gr.DrawString(tableCols.Col_ServicePriceForAll, fontColumn, colorColumnText, new XPoint(topLeft.X + 80, leftYLine - 7));
                    gr.DrawString(String.Format("{0:0.00}", priceForAll), fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, leftYLine - 7));
                    gr.DrawString(String.Format("{0:0.00}", qqs), fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, leftYLine - 7));
                    gr.DrawString(String.Format("{0:0.00}", priceWithQQS), fontColumn, colorColumnText, new XPoint(X5 + 45, leftYLine - 7));
                }

            }
              
            gr.DrawLine(color, new XPoint(topLeft.X, topLeft.Y + heightHeader), new XPoint(topLeft.X, leftYLine));              //Left up to bottom
            gr.DrawLine(color, new XPoint(topRight.X, topLeft.Y + heightHeader), new XPoint(topRight.X, leftYLine));            //Right up to bottom

            gr.DrawLine(color, new XPoint(X1, topLeft.Y + heightHeader), new XPoint(X1, leftYLine - heightOfRow));              //No up to bottom

            gr.DrawLine(color, new XPoint(X2, topLeft.Y + heightHeader), new XPoint(X2, leftYLine - heightOfRow));              //Xizmat turi up to bottom

            gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightHeader), new XPoint(X3, leftYLine - heightOfRow));              //Soni, dona up to bottom

            gr.DrawLine(color, new XPoint(X4, topLeft.Y + heightHeader), new XPoint(X4, leftYLine));                            //Xizmat narxi up to bottom
             
            gr.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightHeader), new XPoint(X3 + widthOfOnePiece, leftYLine));    //Inner xizmat narxi up to bottom

            gr.DrawLine(color, new XPoint(X5, topLeft.Y + heightHeader), new XPoint(X5, leftYLine));                             //QQS up to bottom 
            #endregion
        }

        private void DrawTable2(PdfPage page, XGraphics gr, SellerBuyer rowInfo, SellerBuyer seller, SellerBuyer buyer, double startY)
        {
            var color = XPens.Black;
            var font1 = new XFont("Times New Roman", 11, XFontStyle.Bold);
            var font2 = new XFont("Times New Roman", 9, XFontStyle.Bold);
            int height = 25;
            int innerHeight = 20;

            XPoint topLeft = new XPoint(20, startY);
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

        public class TableColumn
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

        public class TableRow
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

            private double calcQQS = 0.0;

            public TableRow()
            {
                
            }
             
            public TableRow(ServicesInfo info, double qqs)
            {
                Copy(info, qqs);
            }

            public void Copy(ServicesInfo info, double qqs)
            {
                ServiceType = info.name_of_service;
                Count = info.amount_value.ToString();
                PriceForOne = info.amount_value_price;
                calcQQS = qqs;

                CalcPrice();
            }

            private void CalcPrice()
            {
                double count = double.Parse(Count);
                double priceOne = double.Parse(PriceForOne);
                double priceForAll = count * priceOne;
                double qqs = (priceForAll * calcQQS) / 100;
                double priceWithQSS = priceForAll + qqs;

                PriceForAll = String.Format("{0:0.00}", priceForAll);
                QQS = String.Format("{0:0.00}", qqs);
                PriceWithQQS = String.Format("{0:0.00}", priceWithQSS);
            }
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
