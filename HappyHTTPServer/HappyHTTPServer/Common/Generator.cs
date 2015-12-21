namespace HappyHTTPServer.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Generator
    {
        private const string AllSymbols = "0123456789QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm";
        private const string AllDigits = "0123456789";
        private const string AllVowels = "weyuioaWEYUIOA"; // 14
        private const string AllConsonats = "qrtpsdfghjklzxcvbnmQRTPSDFGHJKLZXCVBNM"; // 19
        private const string PhoneCodeBg = "+35988";
        private const int PhoneNumberLenght = 7;

        private static Random rand = new Random();

        public static string GenerateRandomName(int min, int max)
        {
            var name = string.Empty;
            var numberOfSteps = rand.Next(min, max + 1);
            var tempName = new StringBuilder();
            var symbolIndex = 0;
            bool isPreviousLetterVowel = false;

            for (int i = 0; i < numberOfSteps; i++)
            {
                if (i == 0)
                {
                    symbolIndex = rand.Next(10, AllSymbols.Length - 26);
                    var symbol = AllSymbols[symbolIndex];
                    tempName.Append(symbol);
                    if (AllVowels.Contains(symbol))
                    {
                        isPreviousLetterVowel = true;
                    }
                }
                else if (isPreviousLetterVowel)
                {
                    symbolIndex = rand.Next(0, AllConsonats.Length / 2);
                    var symbol = AllConsonats[symbolIndex];
                    tempName.Append(symbol);
                    isPreviousLetterVowel = false;
                }
                else
                {
                    symbolIndex = rand.Next(0, AllVowels.Length / 2);
                    var symbol = AllVowels[symbolIndex];
                    tempName.Append(symbol);
                    isPreviousLetterVowel = true;
                }
            }

            name = tempName.ToString();
            return name;
        }

        public static string GenerateRandomTelephoneNumber()
        {
            var telephoneNumber = string.Empty;
            var digitSecondNumber = "345789";
            var tempNumber = new StringBuilder();
            tempNumber.Append(PhoneCodeBg);

            for (int i = 0; i < PhoneNumberLenght; i++)
            {
                if (i == 0)
                {
                    var digitIndexSecond = rand.Next(0, digitSecondNumber.Length);
                    var secondDigit = digitSecondNumber[digitIndexSecond];
                    tempNumber.Append(secondDigit);
                }
                else
                {
                    var digitIndex = rand.Next(0, AllDigits.Length);
                    var digit = AllDigits[digitIndex];
                    tempNumber.Append(digit);
                }
            }

            telephoneNumber = tempNumber.ToString();
            return telephoneNumber;
        }

        /// <summary>
        /// Random Generater for dates using TimeSpan in days method
        /// </summary>
        /// <param name="min">earliest possible date allowed</param>
        /// <param name="max">latest possible date allowed</param>
        /// <returns>Random Date in the allowed range</returns>
        public static DateTime GenerateRandomDate(DateTime min, DateTime max)
        {
            var timeGap = max - min; // time distance between min and max Date
            var timeGapInDays = (int)timeGap.TotalDays; // converts the timeGap, which is in type TimeSpan to days count and converts it to int in order not to have half days and etc.

            var randomTimePasses = rand.Next(0, timeGapInDays); // gets random number of days form 0 to the timeGapInDays

            var resultDate = min.AddDays(randomTimePasses); // adds the random number of days generated to the minimum value allowed
            return resultDate;
        }

        public static int GetRandomNumber(int min = Int32.MinValue, int max = Int32.MaxValue)
        {
            return rand.Next(min, max + 1);
        }
    }
}
