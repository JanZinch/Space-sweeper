using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace CodeBase.ApplicationLibrary.Utils
{
    public static class TextUtils
    {
        private static readonly List<string> NUMBER_LETTERS = new List<string>
            {"", "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", 
                "aa", "сам продолжай"};

        public static double Round(double rawNumber)
        {
            if (rawNumber < 100) return rawNumber;

            var power10 = (int) Math.Floor(Math.Log(10, rawNumber));
            var roundBase = Math.Pow(10, power10 - 1);
            return (long) (Math.Round(rawNumber / roundBase) * roundBase);
        }

        public static string Truncate(double rawNumber) => Math.Truncate(rawNumber).ToString(CultureInfo.InvariantCulture);

        public static string FormatShort(long rawNumber)
        {
            if (rawNumber < 1000) return rawNumber.ToString();

            var rawPower10 = Mathf.Log(rawNumber, 10) - 0.000000005f;
            var power10 = (int) Mathf.Floor(rawPower10);
            var level = power10 / 3;

            var roundBase = Math.Pow(10, level * 3);
            return string.Format("{0:0.#}", rawNumber / roundBase) + NUMBER_LETTERS[level - 1];
        }
        
        public static string FormatShort(double rawNumber)
        {
            double BaseDel = 1000;
            int currPow;
            if (rawNumber < BaseDel) return Math.Round(rawNumber).ToString(CultureInfo.InvariantCulture);
            for (currPow = 1; currPow <= 26; currPow++)
                if (Math.Pow(BaseDel, currPow) > rawNumber)
                {
                    currPow--;
                    break;
                }
            
            return $"{rawNumber / Math.Pow(BaseDel, currPow):0.#}" + NUMBER_LETTERS[currPow];
        }
        
        public static string FormatLong(long rawNumber)
        {
            return rawNumber.ToString(NumberFormatInfo.CurrentInfo);
        }

        public static string FormatDouble(double rawNumber)
        {
            return rawNumber.ToString(NumberFormatInfo.CurrentInfo);
        }
        
        public static string Cut(string input, int symbolsCount)
        {
            if (input.Length > symbolsCount) input = input.Substring(0, symbolsCount);
            return input;
        }
    }
}