using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaZeroDayCore.Controller
{
    public class F_NumberToWord
    {
        public static string NumberArabic(long p_number)
        {
            string[] array1000 = { " ", " الف ", " الفان ", " الاف " };
            string[] array1000000 = { " ", " مليون ", " مليونان ", " ملايين " };
            string[] array1000000000 = { " ", " مليار ", " ملياران ", " ملايير " };

            if (p_number == 0)
                return " صفر ";
            if (p_number < 0)
                return " ناقس " + NumberArabic(Math.Abs(p_number));
            string words = "";

            if (p_number >= 1000000000)
            {
                int value = 1000000000;
                var and = (p_number % value) > 0 ? " و " : "";
                if (p_number < 3000000000) words += array1000000000[p_number / value] + and;
                else if ((p_number >= 3000000000) && (p_number < 11000000000)) words += NumberArabic_1_999((int)(p_number / value)) + array1000000000[3] + and;
                else words += NumberArabic_1_999((int)(p_number / value)) + array1000000000[1] + and;
                p_number %= value;
            }
            if (p_number >= 1000000)
            {
                int value = 1000000;
                var and = (p_number % value) > 0 ? " و " : "";
                if (p_number < 3000000) words += array1000000[p_number / value] + and;
                else if ((p_number >= 3000000) && (p_number < 11000000)) words += NumberArabic_1_999((int)(p_number / value)) + array1000000[3] + and;
                else words += NumberArabic_1_999((int)(p_number / value)) + array1000000[1] + and;
                p_number %= value;
            }
            if (p_number >= 1000)
            {
                int value = 1000;
                var and = (p_number % value) > 0 ? " و " : "";
                if (p_number < 3000) words += array1000[p_number / value] + and;
                else if ((p_number >= 3000) && (p_number < 11000)) words += NumberArabic_1_999((int)(p_number / value)) + array1000[3] + and;
                else words += NumberArabic_1_999((int)(p_number / value)) + array1000[1] + and;
                p_number %= value;
            }
            words += NumberArabic_1_999((int)p_number);
            return words;
        }
        public static string NumberArabic_1_999(int p_number)
        {
            string[] array0_20 = { "", "واحد", "اثنان", "ثلاثة", "اربعة", "خمسة", "ستة", "سبعة", "ثمانية", "تسعة", "عشرة", "أحد عشر", "اثنا عشر", "ثلاثة عشر", "اربعة عشر", "خمسة عشر", "ستة عشر", "سبعة عشر", "ثمانية عشر", "تسعة عشر" };
            string[] array20_99 = { "", "", "عشرون", "ثلاثون", "اربعون", "خمسون", "ستون", "سبعون", "ثمانون", "تسعون" };
            string[] array100_999 = { " ", " مئة ", " مئتان ", " ثلثمائة ", " اربعمائة ", " خمسمائة ", " ستمائة ", " سبعمائة ", " ثمانمائة ", " تسعمائة " };
            string r = "";
            if ((p_number >= 100) && (p_number < 1000))
            {
                int value = 100;
                var and = (p_number % value) > 0 ? " و " : "";
                r += array100_999[p_number / value] + and;
                p_number %= value;
            }
            if ((p_number > 0) && (p_number < 100))
            {
                if ((p_number > 0) && (p_number < 20)) r += array0_20[p_number];
                else if (p_number < 100)
                {
                    int v10 = p_number / 10;
                    int v1 = p_number % 10;
                    r += v1 == 0 ? array20_99[v10] : array0_20[v1] + " و " + array20_99[v10];
                }
            }
            return r;
        }

        public static string NumberEnglish(long number)
        {
            if (number == 0)
                return "zero";
            if (number < 0)
                return "minus " + NumberEnglish(Math.Abs(number));
            string words = "";
            if ((number / 10000000) > 0)
            {
                words += NumberEnglish(number / 10000000) + " crores ";
                number %= 10000000;
            }
            if ((number / 100000) > 0)
            {
                words += NumberEnglish(number / 100000) + " lacs ";
                number %= 100000;
            }
            if ((number / 1000) > 0)
            {
                words += NumberEnglish(number / 1000) + " thousand ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += NumberEnglish(number / 100) + " hundred ";
                number %= 100;
            }
            if (number > 0)
            {
                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };
                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }
    }
}
