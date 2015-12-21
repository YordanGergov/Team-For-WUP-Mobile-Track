namespace HappyHTTPServer.Common
{
    using System;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// This class helps us generate random data
    /// </summary>
    public static class Generator
    {
        private const string AllSymbols = "0123456789QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm"; // strlen = 62
        private const string AllDigits = "0123456789"; // strlen = 10
        private const string AllVowels = "weyuioaWEYUIOA"; // strlen = 14
        private const string AllConsonats = "qrtpsdfghjklzxcvbnmQRTPSDFGHJKLZXCVBNM"; // strlen = 19

        private static Random rand = new Random();

        /// <summary>
        /// Random Generator for names with minimum and maximum required lenght
        /// </summary>
        /// <param name="min">minimum simbols required for the generated name</param>
        /// <param name="max">maximum simbols required for the generated name</param>
        /// <returns>randomly generated name</returns>
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

        /// <summary>
        /// Random Generator for dates using TimeSpan in days method
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

        /// <summary>
        /// Random Generator for intiger numbers within a range
        /// </summary>
        /// <param name="min">minimum intiger value for the randomly generated number</param>
        /// <param name="max">maximum intiger value for the randomly generated number</param>
        /// <returns>random intiger number</returns>
        public static int GetRandomNumber(int min = Int32.MinValue, int max = Int32.MaxValue)
        {
            return rand.Next(min, max + 1);
        }
    }
}
