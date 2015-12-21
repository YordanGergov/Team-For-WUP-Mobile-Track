namespace HappyHTTPServer.Common
{
    using System;

    public class RandomProvider
    {
        private static readonly Random random = new Random();

        public int GetRandomNumber(int min, int max)
        {
            return random.Next(min, max + 1);
        }
    }
}
