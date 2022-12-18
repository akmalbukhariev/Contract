using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibContract
{
    public static class DigitConverter
    {
        private static string[] soz =  { "", "бир", "икки", "уч", "то`рт", "беш", "олти", "йетти", "саккиз", "то`ққиз" };
        private static string[] soz1 = { "", "о`н", "йигирма", "о`ттиз", "қирқ", "еллик", "олтмиш", "йетмиш", "саксон", "то`қсон" };
        private static string[] soz2 = { "", "бир юз", "икки юз", "уч юз", "то`рт юз", "беш юз", "олти юз", "йетти юз", "саккиз юз", "то`ққиз юз" };
        private static string[] soz3 = { "минг", "бир минг", "икки минг", "уч минг", "то`рт минг", "беш минг", "олти минг", "йетти минг", "саккиз минг", "то`ққиз минг" };
        private static string[] soz4 = { "милион", "бир милион", "икки милион", "уч милион", "то`рт милион", "беш милион", "олти милион", "йетти милион", "саккиз милион", "то`ққиз милион" };
        private static string[] soz5 = { " милиард", "бир милиард", "икки милиард", "уч милиард", "то`рт милиард", "беш милиард", "олти милиард", "йетти милиард", "саккиз милиард", "то`ққиз милиард" };
         
        private static string Raqam(Int64 n)//1=>9
        {
            string s = " " + soz[n] + " "; return s;
        }
        private static string Digit1(Int64 n)//10=>99
        {
            Int64 r = (Int64)n / 10; Int64 h = n % 10; string s = soz1[r] + " " + Raqam(h); return s;
        }
        private static string Digit2(Int64 n)//100=>999
        {
            Int64 r = (Int64)n / 100; Int64 h = n % 100; string s = soz2[r] + " " + Digit1(h); return s;
        }
        private static string Digit3(Int64 n)//1000=>9,999
        {
            Int64 r = (Int64)n / 1000; Int64 h = n % 1000; string s = soz3[r] + " " + Digit2(h); return s;
        }
        private static string Digit4(Int64 n)//10.000=>99.999
        {
            Int64 r = (Int64)n / 10000; Int64 h = n % 10000; string s = soz1[r] + " " + Digit3(h); return s;
        }
        private static string Digit5(Int64 n)//100.000=>999.999
        {
            Int64 r = (Int64)n / 100000; Int64 h = n % 100000; string s = soz2[r] + " " + Digit4(h); return s;
        }
        private static string Digit6(Int64 n)//1000.000=>9.999.999
        {
            Int64 r = (Int64)n / 1000000; Int64 h = n % 1000000; string s = soz4[r] + " " + Digit5(h); return s;
        }
        private static string Digit7(Int64 n)//10.000.000=>99.999.999
        {
            Int64 r = (Int64)n / 10000000; Int64 h = n % 10000000; string s = soz1[r] + " " + Digit6(h); return s;
        }
        private static string Digit8(Int64 n)//100.000.000=>999.999.999
        {
            Int64 r = (Int64)n / 100000000; Int64 h = n % 100000000; string s = soz2[r] + " " + Digit7(h); return s;
        }
        private static string Digit9(Int64 n)//1000.000.000=>9.999.999.999
        {
            Int64 r = (Int64)n / 1000000000; Int64 h = n % 1000000000; string s = soz5[r] + " " + Digit8(h); return s;
        }
        private static string Digit10(Int64 n)//10.000.000.000=>99.999.999.999
        {
            Int64 r = (Int64)n / 10000000000; Int64 h = n % 10000000000; string s = soz1[r] + " " + Digit9(h); return s;
        }
        private static string Digit11(Int64 n)//100.000.000.000=>999.999.999.999
        {
            Int64 r = (Int64)n / 100000000000; Int64 h = n % 100000000000; string s = soz2[r] + " " + Digit10(h); return s;
        }
        public static string Digit2Text(Int64 digit)
        {
            string letter = "";
            if (digit == 0) letter = "нол";
            else if ((digit >= 1) && (digit <= 9)) letter = Raqam(digit);
            else if ((digit >= 10) && (digit <= 99)) letter = Digit1(digit);
            else if ((digit >= 100) && (digit <= 999)) letter = Digit2(digit);
            else if ((digit >= 1000) && (digit <= 9999)) letter = Digit3(digit);
            else if ((digit >= 10000) && (digit <= 99999)) letter = Digit4(digit);
            else if ((digit >= 100000) && (digit <= 999999)) letter = Digit5(digit);
            else if ((digit >= 1000000) && (digit <= 9999999)) letter = Digit6(digit);
            else if ((digit >= 10000000) && (digit <= 99999999)) letter = Digit7(digit);
            else if ((digit >= 100000000) && (digit <= 999999999)) letter = Digit8(digit);
            else if ((digit >= 1000000000) && (digit <= 9999999999)) letter = Digit9(digit);
            else if ((digit >= 10000000000) && (digit <= 99999999999)) letter = Digit10(digit);
            else if ((digit >= 100000000000) && (digit <= 999999999999)) letter = Digit11(digit);
            return letter;
        }
    }
}
