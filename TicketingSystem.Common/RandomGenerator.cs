namespace TicketingSystem.Common
{
    using System;
    using System.Text;

    public class RandomGenerator : TicketingSystem.Common.IRandomGenerator
    {
        private const string Letters = "ABCDEFGHIJKLMNOPQRSTUWXVYZabcdefghijklmnopqrstuwxvyz";

        private Random random;

        public RandomGenerator()
        {
            this.random = new Random();
        }

        public string RandomString(int minLength = 5, int maxLength = 50)
        {
            var result = new StringBuilder();
            var length = this.random.Next(minLength, maxLength + 1);
            for (int i = 0; i <= length; i++)
            {
                result.Append(Letters[this.random.Next(0, Letters.Length)]);
            }

            return result.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            return this.random.Next(min, max + 1);
        }
    }
}
