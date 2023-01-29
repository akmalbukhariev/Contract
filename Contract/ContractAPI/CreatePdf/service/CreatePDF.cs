using LibContract;
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

namespace ContractAPI.CreatePdf.service
{
    public class CreatePDF
    {
        //http://www.pdfsharp.net/wiki/PDFsharpSamples.ashx
        //http://developer.th-soft.com/developer/2015/07/17/pdfsharp-improving-the-xtextformatter-class-measuring-the-height-of-the-text/
         
        public CreateContractInfo ContractInfo { get; set; }
        public UserCompanyInfo UserCompany { get; set; }
        public ClientCompanyInfo ClientCompany { get; set; }
        public ContractTemplate Template { get; set; }
        public List<ServicesInfo> Services { get; set; }

        private List<ContractTemplateJson> JsonList;

        string fontTitleFamily = "Times New Roman";
        string fontTextFamily = "Times New Roman";
        string SaveSignaturePath = string.Empty;
        XSize SizeOfClientPositionOfSignaer;
        XSize SizeOfUserPositionOfSignaer;
        public CreatePDF(string strJson)
        {
            JsonList = JsonConvert.DeserializeObject<List<ContractTemplateJson>>(strJson);
        }
         
        public void CreateContract(string saveFilePath, string rootPath)
        {
            SaveSignaturePath = rootPath + Constants.SaveSignImagePath;

            //GlobalFontSettings.FontResolver = new SegoeWpFontResolver();
            //GlobalFontSettings.DefaultFontEncoding = PdfFontEncoding.WinAnsi;

            var pdf = new PdfDocument();
             
            double qqs = double.Parse(ContractInfo.amount_of_qqs.Replace("%","").Trim());
             
            TableColumn tableCols = new TableColumn()
            {
                Col_No = "№",
                Col_Service = "Хизмат",
                Col_ServiceType = "Хизмат тури",
                Col_Number = "Сони,",
                Col_Piece = "дона",
                Col_ServicePrice = "Хизмат нархи",
                Col_ServicePriceForOne = "Бир донаси",
                Col_For = "учун",
                Col_ServicePriceForAll = "Жами: ",
                Col_QQS = $"ҚҚС {qqs}%",
                Col_Value_QQS = "қиймати (ҚҚС",
                Col_With = "билан)"
            };

            List<TableRow> table1Rows = new List<TableRow>();
            foreach (ServicesInfo item in Services)
            {
                table1Rows.Add(new TableRow(item, qqs));
            }

            List<List<string>> table2Rows = new List<List<string>>();
            table2Rows.Add(new List<string>() { "“БАЖАРУВЧИ”", "“БУЮРТМАЧИ”" });
            table2Rows.Add(new List<string>() { ClientCompany.company_name, UserCompany.company_name});
            table2Rows.Add(new List<string>() { $"Манзил: {ClientCompany.address_of_company}", $"Манзил: {UserCompany.address_of_company}" });
            table2Rows.Add(new List<string>() { $"Ҳисоб рақами: {ClientCompany.account_number}", $"Ҳисоб рақами: {UserCompany.account_number}" });
            table2Rows.Add(new List<string>() { $"{ClientCompany.name_of_bank}", $"{UserCompany.name_of_bank}" });
            table2Rows.Add(new List<string>() { $"Банк коди: {ClientCompany.bank_code}", $"Банк коди: {UserCompany.bank_code}" });
            table2Rows.Add(new List<string>() { $"СТИР: {ClientCompany.stir_of_company}", $"СТИР: {UserCompany.stir_of_company}" });
            table2Rows.Add(new List<string>() { "ОКЭД: ", "ОКЭД: " });
            table2Rows.Add(new List<string>() { $"Телефон: {ClientCompany.company_phone_number}", $"Телефон: {UserCompany.company_phone_number}" });
            table2Rows.Add(new List<string>() { "", "" });
            table2Rows.Add(new List<string>() { $"{ClientCompany.position_of_signer} _____________ {ClientCompany.name_of_signer}.", 
                                                $"{UserCompany.position_of_signer} _____________ {UserCompany.name_of_signer}." });
            table2Rows.Add(new List<string>() { "(имзо)", "(имзо)" });
             
            CreatePage(JsonList, 0, 0, pdf, tableCols, table1Rows, table2Rows, false, false);

            //string filename = "Contract.pdf";
            pdf.Save(saveFilePath);
            //Console.WriteLine("Done........");

            //Process proc = new Process();
            //proc.StartInfo.UseShellExecute = true;
            //proc.StartInfo.FileName = filename;
            //proc.Start();

            //Environment.Exit(0);
        }

        private void CreatePage(List<ContractTemplateJson> jsonList, int index, int chIndex, PdfDocument pdf, TableColumn tableCols = null, List<TableRow> table1Rows = null,List<List<string>> table2Rows = null, bool drawedTitle = false, bool drawedMainText = true)
        {
            var page = pdf.AddPage();
            var gr = XGraphics.FromPdfPage(page);
             
            var fontTitle = new XFont(fontTitleFamily, 11, XFontStyle.Bold);
            var fontText = new XFont(fontTextFamily, 7, XFontStyle.Regular);

            XRect layoutText = new XRect(20, 50, page.Width - 35, page.Height - 20);
            XRect layoutNextText = layoutText;

            XSolidBrush textColor = XBrushes.Black;
            XTextFormatterEx calcHegihtOfText = new XTextFormatterEx(gr);
            XTextFormatter drawText = new XTextFormatter(gr);

            SizeOfClientPositionOfSignaer = gr.MeasureString(ClientCompany.position_of_signer, fontText);
            SizeOfUserPositionOfSignaer = gr.MeasureString(UserCompany.position_of_signer, fontText);

            double lineHeight = 12.181640625;

            if (!drawedMainText)
            {
                string strContractNumber = ContractTextEditor.ContractWidthNumber(ContractNumberWorker.ExtractContractNumber(ContractInfo.contract_number));
                string strDate = ContractTextEditor.TodaysDate();
                string strMainText = ContractTextEditor.MainText(UserCompany, ClientCompany);

                drawText.Alignment = XParagraphAlignment.Center;
                drawText.DrawString(strContractNumber, fontTitle, textColor, layoutNextText);

                layoutNextText.Y += lineHeight + lineHeight;
                drawText.Alignment = XParagraphAlignment.Left;
                drawText.DrawString(strDate, fontText, textColor, layoutNextText);

                drawText.Alignment = XParagraphAlignment.Right;
                drawText.DrawString(Template.contract_address, fontText, textColor, layoutNextText);

                layoutNextText.Y += lineHeight + lineHeight;
                drawText.Alignment = XParagraphAlignment.Justify;
                drawText.DrawString(strMainText, fontText, textColor, layoutNextText);

                double textMainHeight = calcHegihtOfText.ClacHeightOfText(fontText, layoutNextText, strMainText);
                layoutNextText.Y += textMainHeight + lineHeight * 2;
            }

            for (int i = index; i < jsonList.Count; i++)
            {
                double lastYOfTable = layoutNextText.Y;

                ContractTemplateJson item = jsonList[i];
                if (item.IsContractServiceDetailsButton && !item.IsVisibleAddButton)
                {
                    if (layoutNextText.Y >= page.Height - lineHeight * 4)
                    {
                        gr = DrawTable1(pdf, null, gr, tableCols, table1Rows, layoutNextText.Y, ref lastYOfTable);
                    }
                    else
                    {
                        gr = DrawTable1(pdf, page, gr, tableCols, table1Rows, layoutNextText.Y, ref lastYOfTable);
                    }

                    drawText = new XTextFormatter(gr);
                    layoutNextText.Y = lastYOfTable + lineHeight;
                    drawText.DrawString(ContractTextEditor.CalcAllService(table1Rows, ContractInfo.contract_currency), fontText, textColor, layoutNextText);

                    layoutNextText.Y += lineHeight * 2;
                }
                else if (item.IsContractInfoButton && !item.IsVisibleAddButton)
                {
                    if (layoutNextText.Y >= page.Height - lineHeight * 4)
                    {
                        gr = DrawTable2(pdf, null, gr, table2Rows, layoutNextText.Y, ref lastYOfTable);
                    }
                    else
                    {
                        gr = DrawTable2(pdf, page, gr, table2Rows, layoutNextText.Y, ref lastYOfTable);
                    }

                    drawText = new XTextFormatter(gr);
                    layoutNextText.Y = lastYOfTable + lineHeight * 2;
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
                            CreatePage(jsonList, i, 0, pdf, tableCols, table1Rows, table2Rows);
                            break;
                        }
                        
                        drawText.Alignment = XParagraphAlignment.Center;
                        drawText.DrawString(strTitle, fontTitle, textColor, layoutNextText);

                        layoutNextText.Y += textHeight + lineHeight;
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
                                CreatePage(jsonList, i, j, pdf, tableCols, table1Rows, table2Rows, true);
                            else
                                CreatePage(jsonList, i, j, pdf, tableCols, table1Rows, table2Rows);
                            return;
                        }

                        drawText.Alignment = XParagraphAlignment.Justify;
                        drawText.DrawString(strChild, fontText, textColor, layoutNextText);

                        layoutNextText.Y += textHeight;
                    }

                    chIndex = 0;
                    drawedTitle = false;
                    layoutNextText.Y += lineHeight * 2;
                }
            }
        }

        private XGraphics DrawTable1(PdfDocument pdf, PdfPage oldPage, XGraphics oldGr, TableColumn tableCols, List<TableRow> tableRows, double startY, ref double endYOfTable, int rowIndex = 0, bool drawedHeader = false)
        {
            var page = oldPage == null? pdf.AddPage() : oldPage;
            var gr = oldPage == null ? XGraphics.FromPdfPage(page) : oldGr;
              
            double lineHeight = 12.181640625;

            endYOfTable = startY;
            if (startY >= page.Height - lineHeight * 4)
            {
                double lastY = 0.0;
                gr = DrawTable1(pdf, null, gr, tableCols, tableRows, 20, ref lastY);
                return gr;
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
            var fontColumn = new XFont(fontTitleFamily, 7, XFontStyle.Regular);
            var colorColumnText = XBrushes.Black;

            int widthOfNo = 30;
            double X1 = topLeft.X + widthOfNo;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X1, topLeft.Y), new XPoint(X1, topLeft.Y + heightHeader));  //No

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_No, fontColumn, colorColumnText, new XPoint(topLeft.X + (widthOfNo / 2) - 5, topLeft.Y + heightHeader / 2));

            int widthOfServiceType = 180;
            double X2 = X1 + widthOfServiceType;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X2, topLeft.Y), new XPoint(X2, topLeft.Y + heightHeader));  //Xizmat turi

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_ServiceType, fontColumn, colorColumnText, new XPoint(X1 + (widthOfServiceType / 2) - 20, topLeft.Y + heightHeader / 2));

            int widthOfCountValue = 50;
            double X3 = X2 + widthOfCountValue;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X3, topLeft.Y), new XPoint(X3, topLeft.Y + heightHeader)); //Soni, dona

            if (!drawedHeader)
            {
                gr.DrawString(tableCols.Col_Number, fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, topLeft.Y + (heightHeader / 2) - 5));
                gr.DrawString(tableCols.Col_Piece, fontColumn, colorColumnText, new XPoint(X2 + (widthOfCountValue / 2) - 10, topLeft.Y + (heightHeader / 2) + 5));
            }

            int widthOfServicePrice = 120;
            double X4 = X3 + widthOfServicePrice;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X4, topLeft.Y), new XPoint(X4, topLeft.Y + heightHeader)); //Xizmat narxi 

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_ServicePrice, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 20, topLeft.Y + (heightHeader / 2) - 12));

            int heightOfServicePrice = 20;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightOfServicePrice), new XPoint(X4, topLeft.Y + heightOfServicePrice)); //Inner Xizmat narxi

            if (!drawedHeader)
            {
                gr.DrawString(tableCols.Col_ServicePriceForOne, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 50, topLeft.Y + (heightHeader / 2) + 7));
                gr.DrawString(tableCols.Col_For, fontColumn, colorColumnText, new XPoint(X3 + (widthOfServicePrice / 2) - 40, topLeft.Y + (heightHeader / 2) + 17));
            }

            double widthOfOnePiece = widthOfServicePrice / 2;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightOfServicePrice), new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightHeader));

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_ServicePriceForAll, fontColumn, colorColumnText, new XPoint(X3 + widthOfOnePiece + (widthOfOnePiece / 2) - 10, topLeft.Y + (heightHeader / 2) + 10));

            int widthOfQQS = 50;
            double X5 = X4 + widthOfQQS;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X5, topLeft.Y), new XPoint(X5, topLeft.Y + heightHeader)); //QQS

            if (!drawedHeader)
                gr.DrawString(tableCols.Col_QQS, fontColumn, colorColumnText, new XPoint(X4 + widthOfQQS / 2 - 18, topLeft.Y + (heightHeader / 2) + 5));

            double X6 = X5 + page.Width;
            if (!drawedHeader)
                gr.DrawLine(color, new XPoint(X6, topLeft.Y), new XPoint(X6, topLeft.Y + heightHeader)); //Xizmat qiymati(QQS bilan)

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

            var grOld = gr;
            for (int i = rowIndex; i < tableRows.Count; i++)
            {
                double lastY = leftYLine + heightOfRow;

                if (lastY >= page.Height - lineHeight * 4)
                {
                    gr.DrawLine(color, new XPoint(X1, topLeft.Y + heightHeader), new XPoint(X1, leftYLine));    //No up to bottom
                    gr.DrawLine(color, new XPoint(X2, topLeft.Y + heightHeader), new XPoint(X2, leftYLine));    //Xizmat turi up to bottom
                    gr.DrawLine(color, new XPoint(X3, topLeft.Y + heightHeader), new XPoint(X3, leftYLine));    //Soni, dona up to bottom
                    
                    gr = DrawTable1(pdf, null, gr, tableCols, tableRows, 20, ref lastY, i, true);
                    endYOfTable = lastY;
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
                    
                    endYOfTable = leftYLine;
                }
            } 

            grOld.DrawLine(color, new XPoint(topLeft.X, topLeft.Y + heightHeader), new XPoint(topLeft.X, leftYLine));                          //Left up to bottom
            grOld.DrawLine(color, new XPoint(topRight.X, topLeft.Y + heightHeader), new XPoint(topRight.X, leftYLine));                        //Right up to bottom
            grOld.DrawLine(color, new XPoint(X1, topLeft.Y + heightHeader), new XPoint(X1, leftYLine - heightOfRow));                          //No up to bottom
            grOld.DrawLine(color, new XPoint(X2, topLeft.Y + heightHeader), new XPoint(X2, leftYLine - heightOfRow));                          //Xizmat turi up to bottom
            grOld.DrawLine(color, new XPoint(X3, topLeft.Y + heightHeader), new XPoint(X3, leftYLine - heightOfRow));                          //Soni, dona up to bottom
            grOld.DrawLine(color, new XPoint(X4, topLeft.Y + heightHeader), new XPoint(X4, leftYLine));                                        //Xizmat narxi up to bottom
            grOld.DrawLine(color, new XPoint(X3 + widthOfOnePiece, topLeft.Y + heightHeader), new XPoint(X3 + widthOfOnePiece, leftYLine));    //Inner xizmat narxi up to bottom
            grOld.DrawLine(color, new XPoint(X5, topLeft.Y + heightHeader), new XPoint(X5, leftYLine));                                        //QQS up to bottom 
            #endregion

            return gr;
        }
         
        private XGraphics DrawTable2(PdfDocument pdf, PdfPage oldPage, XGraphics oldGr, List<List<string>> list, double startY, ref double endYOfTable, int rowIndex = 0)
        {
            var page = oldPage == null ? pdf.AddPage() : oldPage;
            var gr = oldPage == null ? XGraphics.FromPdfPage(page) : oldGr;

            double lineHeight = 12.181640625;

            endYOfTable = startY;
            if (startY >= page.Height - lineHeight * 4)
            {
                double lastY = 0.0;
                gr = DrawTable2(pdf, null, gr, list, 20, ref lastY);
                return gr;
            }

            var color = XPens.Black;
            var textColor = XBrushes.Black;
            var font1 = new XFont(fontTitleFamily, 11, XFontStyle.Bold);
            var font2 = new XFont(fontTextFamily, 9, XFontStyle.Regular);

            int heightOfRow = 20;

            XPoint topLeft = new XPoint(20, startY);
            XPoint topRight = new XPoint(page.Width - 20, topLeft.Y);
            //XPoint bottomRight = new XPoint(page.Width - 20, topLeft.Y + heightOfRow);
            //XPoint bottomLeft = new XPoint(topLeft.X, topLeft.Y + heightOfRow);

            double mX = (topLeft.X + topRight.X) / 2;
            double leftYLine = topLeft.Y;// + heightOfRow;
            for (int i = rowIndex; i < list.Count; i++)
            {
                double lastY = leftYLine + heightOfRow;
                if (lastY >= page.Height - lineHeight * 4)
                {
                    if (i != 0)
                        gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topRight.X, leftYLine));                 //Top Left to Top right
                    
                    gr = DrawTable2(pdf, null, gr, list, 20, ref lastY, i);
                    endYOfTable = lastY;
                    break;
                }

                List<string> item = list[i];
                gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topRight.X, leftYLine));                 //Top Left to Top right
                
                gr.DrawLine(color, new XPoint(topLeft.X, leftYLine), new XPoint(topLeft.X, leftYLine + heightOfRow));    //Top Left to bottom
                gr.DrawLine(color, new XPoint(topRight.X, leftYLine), new XPoint(topRight.X, leftYLine + heightOfRow));  //Top Right to bottom 

                gr.DrawLine(color, new XPoint(mX - 10, leftYLine), new XPoint(mX - 10, leftYLine + heightOfRow));        //Top left middle to bottom
                gr.DrawLine(color, new XPoint(mX + 10, leftYLine), new XPoint(mX + 10, leftYLine + heightOfRow));        //Top right middle to bottom

                if (i == 0)
                {
                    gr.DrawString(item[0], font1, textColor, new XPoint(topLeft.X + 50, leftYLine + heightOfRow - 5));
                    gr.DrawString(item[1], font1, textColor, new XPoint(mX + 50, leftYLine + heightOfRow - 5));
                }
                else
                {
                    if (item[0].Contains("(имзо)"))
                    {
                        //gr.DrawString(item[0], font2, textColor, new XPoint(mX - 30, leftYLine));               //imzo
                        //gr.DrawString(item[1], font2, textColor, new XPoint(topRight.X - 30, leftYLine));       //imzo
                    }
                    else
                    {
                        gr.DrawString(item[0], font2, textColor, new XPoint(topLeft.X + 10, leftYLine + heightOfRow - 5));
                        gr.DrawString(item[1], font2, textColor, new XPoint(mX + 15, leftYLine + heightOfRow - 5));
                    }
                }
                
                if (i == list.Count - 1)
                {
                    XPoint point1 = new XPoint(topLeft.X, leftYLine + heightOfRow);
                    XPoint point2 = new XPoint(topRight.X, leftYLine + heightOfRow);
                      
                    double xSign1 = point1.X + SizeOfClientPositionOfSignaer.Width + 20;
                    double xSign2 = mX + SizeOfUserPositionOfSignaer.Width + 25;

                    DrawSignature(xSign1, xSign2, point1.Y - 50, gr);
                    gr.DrawString(item[0], font2, textColor, new XPoint(xSign1, point1.Y - 10));   //imzo
                    gr.DrawString(item[1], font2, textColor, new XPoint(xSign2, point1.Y - 10));   //imzo 
                    gr.DrawLine(color, point1, point2);                                            //Top Left to Top right
                }

                leftYLine += heightOfRow;
                endYOfTable = leftYLine;
            }

            return gr;
        }

        private void DrawSignature(double x1, double x2, double y, XGraphics gr)
        {
            double width = 30;
            double height = 30;
            string saveUserSignFile = $"{SaveSignaturePath}{ContractInfo.user_phone_number}_sign.png";
            string saveClientSignFile = $"{SaveSignaturePath}{ContractInfo.contragent_phone_number}_sign.png";

            bool is_approved = ContractInfo.is_approved == 1 ? true : false;
            bool is_approved_contragent = ContractInfo.is_approved_contragent == 1 ? true : false;

            if (File.Exists(saveUserSignFile) && is_approved)
            {
                XImage image = XImage.FromFile(saveUserSignFile);
                gr.DrawImage(image, x1, y, width, height);
            }

            if (File.Exists(saveClientSignFile) && is_approved_contragent)
            {
                XImage image = XImage.FromFile(saveClientSignFile);
                gr.DrawImage(image, x2, y, width, height);
            }
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
    }
}
