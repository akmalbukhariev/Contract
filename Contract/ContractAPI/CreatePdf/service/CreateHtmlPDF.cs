using Newtonsoft.Json;
using LibContract.HttpModels;
using System.Collections.Generic;
using LibContract;
using System.IO;
using System;
using System.Diagnostics;

namespace ContractAPI.CreatePdf.service
{
    public class CreateHtmlPDF
    {
        public CreateContractInfo ContractInfo { get; set; }
        public UserCompanyInfo UserCompany { get; set; }
        public ClientCompanyInfo ClientCompany { get; set; }
        public ContractTemplate Template { get; set; }
        public List<ServicesInfo> Services { get; set; }

        private List<ContractTemplateJson> JsonList;

        string SaveSignaturePath = string.Empty;
        public CreateHtmlPDF(string strJson)
        {
            JsonList = JsonConvert.DeserializeObject<List<ContractTemplateJson>>(strJson);
        }

        public void CreateContract(string saveFilePath, string rootPath)
        {
            SaveSignaturePath = rootPath + Constants.SaveSignImagePath;

            double qqs = double.Parse(ContractInfo.amount_of_qqs.Replace("%", "").Trim());

            List<TableRow> tableRows = new List<TableRow>();
            foreach (ServicesInfo item in Services)
            {
                tableRows.Add(new TableRow(item, qqs));
            }

            string strHtml = "";
            strHtml += StartPoint();
            strHtml +=DateAndAddress();
            strHtml +=Maintext();

            for (int i = 0; i < JsonList.Count; i++)
            {
                ContractTemplateJson item = JsonList[i];
                if (item.IsContractServiceDetailsButton && !item.IsVisibleAddButton)
                {
                    strHtml += CreateTable1(tableRows, qqs.ToString());
                }
                else if (item.IsContractInfoButton && !item.IsVisibleAddButton)
                {
                    strHtml += Createtable2();
                }
                else
                {
                    strHtml += ContractTextEditor.CreateTitle($"{item.Title}. {item.Description}") + Environment.NewLine;
                    for (int j = 0; j < item.Child.Count; j++)
                    {
                        strHtml += ContractTextEditor.CreateDescription($"{item.Child[j].Title}",$"{item.Child[j].Description}") + Environment.NewLine;
                    }
                }
            }

            strHtml +=CreateSignature();
            strHtml += EndPoint();

            File.WriteAllText(saveFilePath, strHtml); 
        }   

        private string StartPoint()
        {
            string strContractNumber = ContractTextEditor.ContractWidthNumber(ContractNumberWorker.ExtractContractNumber(ContractInfo.contract_number));
            string result = "<!DOCTYPE html>" + Environment.NewLine +
                                "<html lang = \"en\">" + Environment.NewLine +
                                     "<head>" + Environment.NewLine +
                                         "<meta http-equiv = \"Content-Type\" content = \"text/html; charset=UTF-8\">" + Environment.NewLine +
                                             "<meta name = \"viewport\" content = \"width=device-width, initial-scale=1.0\">" + Environment.NewLine +
                                                $"<title> {strContractNumber} </title>" + Environment.NewLine +
                                                $"<link rel = \"stylesheet\" href = \"styles/styles.css\">" + Environment.NewLine +
                                                "</head>" + Environment.NewLine +
                                                "<body>" + Environment.NewLine +
                                                    "<div class=\"main\" style=\"text-align: justify;\">" + Environment.NewLine +
                                                         "<div class=\"headmain\">" + Environment.NewLine +
                                                             $"<div class=\"headtext\"><h3 class=\"number\">{strContractNumber}</h3></div>" + Environment.NewLine +
                                                         "</div>" + Environment.NewLine;
            return result;
        }

        private string DateAndAddress()
        {
            string result = "<div class=\"infomain\">" + Environment.NewLine +
                                 $"<div class=\"date\"><span><b>{ContractTextEditor.ContractCreatedDate(ContractInfo.created_date)}</b></span></div>" + Environment.NewLine +
                                 $"<div class=\"address\"><span><b>{Template.contract_address}</b></span></div>" + Environment.NewLine +
                            "</div>" + Environment.NewLine;
            return result;
        }

        private string Maintext()
        {
            string result = $"<div class=\"htext\"><span class=\"servicetext\">{ContractTextEditor.MainText(UserCompany, ClientCompany)}</span></div><br>" + Environment.NewLine;
            return result;
        }

        private string CreateTable1(List<TableRow> rows, string qqs)
        {
            string result = "";

            result = "<table>" + Environment.NewLine +
                        "<tbody>"  + Environment.NewLine +
                           "<tr>" + Environment.NewLine +
                               "<th rowspan = \"2\"><b>№</b></th>" + Environment.NewLine +
                                     "<th rowspan = \"2\" style = \"width: 400px;\"><b> Хизмат тури </b></th>" + Environment.NewLine +
                                              "<th rowspan = \"2\"><b> Сони, дона </b></th>" + Environment.NewLine +
                                                     "<th colspan = \"2\" style = \"text-align: center\"><b> Хизмат нархи </b></th>" + Environment.NewLine +
                                                              $"<th rowspan = \"2\" style = \"width: 150px; text-align: center\"><b> ҚҚС {qqs} %</b></th>" + Environment.NewLine +
                                                                       "<th rowspan = \"2\" style = \"width: 150px; text-align: center\"><b> Хизмат киймати(ҚҚС билан) </b></th>" + Environment.NewLine +
                                                                            "</tr>" + Environment.NewLine +
                                                                                "<tr>" + Environment.NewLine +
                                                                                   "<th style = \"width: 150px; text-align: center\"><b> Бир донаси учун</b></th>" + Environment.NewLine +
                                                                                   "<th style = \"width: 150px; text-align: center\"><b> Жами </b></th>" + Environment.NewLine +
                                                                                "</tr>" + Environment.NewLine;

            double priceForAll = 0.0;
            double qqsCalc = 0.0;
            double priceWithQQS = 0.0;

            foreach (TableRow row in rows)
            {
                priceForAll += double.Parse(row.PriceForAll);
                qqs += double.Parse(row.QQS.Replace("%", ""));
                priceWithQQS += double.Parse(row.PriceWithQQS);
            }

            string strRow = "";
            for (int i = 0; i < rows.Count; i++)
            {
                strRow += "<tr>" + Environment.NewLine +
                         $"<td>{i + 1}</td>" + Environment.NewLine +
                         $"<td>{rows[i].ServiceType} </td>" + Environment.NewLine +
                         $"<td>{rows[i].Count} </td>" + Environment.NewLine +
                         $"<td>{rows[i].PriceForOne} </td>" + Environment.NewLine +
                         $"<td>{rows[i].PriceForAll} </td>" + Environment.NewLine +
                         $"<td>{rows[i].QQS}  </td>" + Environment.NewLine +
                         $"<td>{rows[i].PriceWithQQS} </td>" + Environment.NewLine +
                         "</tr>" + Environment.NewLine;

                if (i == rows.Count - 1)
                {
                    strRow += "<tr>" + Environment.NewLine +
                              "<td colspan = \"4\" style = \"text-align: right\"><b> Жами: </b></td>" + Environment.NewLine +
                              $"<td><b> {priceForAll} </b></td>" + Environment.NewLine +
                              $"<td><b> {qqsCalc} </b></td>" + Environment.NewLine +
                              $"<td><b> {priceWithQQS} </b></td>" + Environment.NewLine +
                              "</tr> " + Environment.NewLine;
                }
            }

            strRow += "</tbody></table>" + Environment.NewLine;
            result += strRow;
            result += $"{ContractTextEditor.CalcAllService(rows, ContractInfo.contract_currency)}" + Environment.NewLine;
            result += "<pre>"+
                      Environment.NewLine +
                      Environment.NewLine +
                      "</pre>";
            return result;
        }

        private string Createtable2()
        {
            string result = "<div class=\"bl\">" + Environment.NewLine +
                                "<div class=\"bl1\">" + Environment.NewLine +
                                    "<h3 style = \"text-align: center;\"> \"БАЖАРУВЧИ\" </h3><br>" + Environment.NewLine +

                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> {UserCompany.company_name}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Манзил: {UserCompany.address_of_company}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Ҳисоб рақами: {UserCompany.account_number}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> {UserCompany.name_of_bank}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Банк коди: {UserCompany.bank_code}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> ҚҚС рақами: {UserCompany.qqs_number}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> СТИР: {UserCompany.stir_of_company}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> ИФУТ  No </p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Телефон: {UserCompany.company_phone_number}</p>" + Environment.NewLine +
                                "</div>" + Environment.NewLine +
                                "<div class=\"bl2\">" + Environment.NewLine +
                                    "<h3 style = \"text-align: center;\"> \"БУЮРТМАЧИ\" </h3><br>" + Environment.NewLine +

                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> {ClientCompany.company_name}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Манзил: {ClientCompany.address_of_company}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Ҳисоб рақами: {ClientCompany.account_number}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> {ClientCompany.name_of_bank}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Банк коди: {ClientCompany.bank_code}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> ҚҚС рақами: {ClientCompany.qqs_number}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> СТИР: {ClientCompany.stir_of_company}</p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> ИФУТ  No </p><br>" + Environment.NewLine +
                                    $"<p style = \"font-size: 18px; border-bottom: 1px dotted black;\"> Телефон: {ClientCompany.company_phone_number}</p>" + Environment.NewLine +
                                "</div>" + Environment.NewLine +
                              "</div>" + Environment.NewLine;
              
            return result;
        }

        private string CreateSignature()
        {
            string saveUserSignFile = $"{SaveSignaturePath}{ContractInfo.user_phone_number}_sign.png";
            string saveClientSignFile = $"{SaveSignaturePath}{ContractInfo.contragent_phone_number}_sign.png";

            bool is_approved = ContractInfo.is_approved == 1 ? true : false;
            bool is_approved_contragent = ContractInfo.is_approved_contragent == 1 ? true : false;

            if (!is_approved) saveUserSignFile = "";
            if (!is_approved_contragent) saveClientSignFile = "";

            string userAccountName = UserCompany.is_accountant_provided == 1? "Бухгалтер: " +UserCompany.accountant_name : "";
            string clientAccountName = ClientCompany.is_accountant_provided == 1 ? "Бухгалтер: " + ClientCompany.accountant_name : "";

            string userCounselName = UserCompany.is_legal_counsel_provided == 1 ? "Юрист: " + UserCompany.counsel_name : "";
            string clientCounselName = ClientCompany.is_legal_counsel_provided == 1 ? "Юрист: " + ClientCompany.counsel_name : "";

            string result = "<div style=\"width: 100%\">" + Environment.NewLine +
                            $"<img class=\"imzo\" src=\"{saveUserSignFile}\"/>" + Environment.NewLine +
                            "<div style=\"float: left; width: 50%; margin-top: 30px; padding: 5px\">" + Environment.NewLine +
                                "<div style=\"font-size: 18px\">" + Environment.NewLine +
                                    $"<span style=\"float: left; width: 50%\">{UserCompany.position_of_signer}<br/>{UserCompany.name_of_signer}.</span>" + Environment.NewLine +
                               "</div>" + Environment.NewLine +
                            "</div>" + Environment.NewLine +
                            $"<img class=\"imzo\" src=\"{saveClientSignFile}\"/>" + Environment.NewLine +
                            "<div style=\"float: right; width: 50%; margin-top: 20px; padding: 15px\">" + Environment.NewLine +
                                "<div style=\"font-size: 18px\">" + Environment.NewLine +
                                    $"<span style=\"float: left; width: 50%\">{ClientCompany.position_of_signer}<br/>{ClientCompany.name_of_signer}.</span>" + Environment.NewLine +
                                "</div> " + Environment.NewLine +
                            "</div>" + Environment.NewLine +
                        "</div>" + Environment.NewLine +
                        "<div style=\"width: 100%\">"+ Environment.NewLine +
			            	"<div style=\"float: left; width: 50%; margin-top: 30px; padding: 5px\">"+ Environment.NewLine +
			            		"<div style=\"font-size: 18px\">"+ Environment.NewLine +
			            			$"<span style=\"float: left; width: 50%\">{userAccountName}</span>"+ Environment.NewLine +
			            		"</div>"+ Environment.NewLine +
			            	"</div>"+ Environment.NewLine +
			            	"<div style=\"float: right; width: 50%; margin-top: 30px; padding: 5px\">"+ Environment.NewLine +
			            		"<div style=\"font-size: 18px; padding-left: 10px\">"+ Environment.NewLine +
			            			$"<span style=\"float: left; width: 50%\">{clientAccountName}</span> "+ Environment.NewLine +
                                "</div>"+ Environment.NewLine +
                            "</div> "+ Environment.NewLine +
                        "</div>" + Environment.NewLine +
                        "<div style=\"width: 100%\">"+ Environment.NewLine +
			            "<div style=\"float: left; width: 50%; margin-top: 30px; padding: 5px\">"+ Environment.NewLine +
			            		"<div style=\"font-size: 18px\">" + Environment.NewLine +
			            			$"<span style=\"float: left; width: 50%\">{userCounselName}</span>" + Environment.NewLine +
			            		"</div>" + Environment.NewLine +
			            	"</div>" + Environment.NewLine +
			            	"<div style=\"float: right; width: 50%; margin-top: 30px; padding: 5px\">" + Environment.NewLine +
			            		"<div style=\"font-size: 18px; padding-left: 10px\">" + Environment.NewLine +
			            			$"<span style=\"float: left; width: 50%\">{clientCounselName}</span>"+ Environment.NewLine +
			            		 "</div>" + Environment.NewLine +
			            	"</div>" + Environment.NewLine +
			            "</div>" + Environment.NewLine +
                        "<div style=\"margin-bottom: 140px;\"></div>";

            return result;
        }

        private string EndPoint()
        {
            string result = "</div></body></html>";

            return result;
        }
    }
}
