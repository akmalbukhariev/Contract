using LibContract.HttpModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContractAPI.CreatePdf.service
{
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
