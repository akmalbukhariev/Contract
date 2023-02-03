﻿using LibContract;
using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace TestAPI
{
    public static class ContractTextEditor
    {
        public static string ContractWidthNumber(string contractNumber)
        {
            return $"ШАРТНОМА № {contractNumber}";    
        }

        public static string TodaysDate()
        {
            int year = DateTime.Now.Year;
            string month = DateTime.Now.ToString("MMMM", new System.Globalization.CultureInfo("uz-Cyrl"));
            int day = DateTime.Now.Day;
             
            return $"{year} йил {day} {month}";
        }

        public static string MainText(UserCompanyInfo user, ClientCompanyInfo client)
        {
            return $"{user.company_name} номидан {client.document} асосида иш олиб борувчи ва бундан сўнг матнда “Буюртмачи” деб аталувчи - унинг {user.position_of_signer} {user.name_of_signer}." +
                   $"бир томондан ва {client.company_name} номидан {client.document} асосида иш юритувчи, бундан сўнг “Бажарувчи” деб аталувчи - унинг {client.position_of_signer} {client.name_of_signer}."+ 
                   $"иккинчи томондан, мазкур шартномани туздилар:";
        }

        public static string CalcAllService(List<CreatePDF.TableRow> tableRows, string contractCurrency)
        {
            double priceForAll = 0.0;
            double qqs = 0.0;
            double priceWithQQS = 0.0;

            foreach (CreatePDF.TableRow row in tableRows)
            {
                priceForAll += double.Parse(row.PriceForAll);
                qqs += double.Parse(row.QQS.Replace("%", ""));
                priceWithQQS += double.Parse(row.PriceWithQQS);
            }

            string[] dList = string.Format("{0:0.00}", priceWithQQS).Split('.');
            string cent = dList.Length == 2 ? dList[1] : "00";

            string textDigits = FirstCharToUpper(DigitConverter.Digit2Text((long)priceWithQQS));
            return $"Жами шартнома суммаси ҚҚС билан: {textDigits} {contractCurrency} {cent} тийин";
        }

        public static string FirstCharToUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }

            return $"{input[0].ToString().ToUpper()}{input.Substring(1)}";
        }
    }
}
