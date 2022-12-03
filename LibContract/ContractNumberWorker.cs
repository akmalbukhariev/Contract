using System;
using System.Collections.Generic;
using System.Text;

namespace LibContract
{
    public class ContractNumberWorker
    {
        public static string MakeSequenceNumber(string seqNumber)
        {
            int seq = int.Parse(seqNumber);
            seq = seq + 1;
            int length = seq.ToString().Length;

            string res = "";
            for (int i = 0; i < 5 - length; i++)
            {
                res += "0";
            }

            res += seq.ToString();

            return res;
        }

        public static string ExtractContractNumber(string strContractnumber)
        {
            string[] strList = strContractnumber.Split('_');

            if (strList.Length == 3)
            {
                return $"{strList[1]}-{strList[2]}";
            }
            else if (strList.Length == 2)
            {
                return strList[1];
            }

            return "";
        }
    }
}
