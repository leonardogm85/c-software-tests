using Bogus.DataSets;
using System;
using System.Linq;

namespace NerdStore.WebApp.Tests.Config
{
    public static class InternetExtensions
    {
        public static string Password(
            this Internet internet,
            int minLength,
            int maxLength,
            bool includeUppercase = true,
            bool includeNumber = true,
            bool includeSymbol = true)
        {
            if (internet == null)
            {
                throw new ArgumentNullException(nameof(internet));
            }

            if (minLength < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(minLength));
            }

            if (maxLength < minLength)
            {
                throw new ArgumentOutOfRangeException(nameof(maxLength));
            }

            var random = internet.Random;

            var password = random.Char('a', 'z').ToString();

            if (password.Length < maxLength && includeUppercase)
            {
                password += random.Char('A', 'Z').ToString();
            }

            if (password.Length < maxLength && includeNumber)
            {
                password += random.Char('0', '9').ToString();
            }

            if (password.Length < maxLength && includeSymbol)
            {
                password += random.Char('!', '/').ToString();
            }

            if (password.Length < minLength)
            {
                password += random.String2(minLength - password.Length);
            }

            if (password.Length < maxLength)
            {
                password += random.String2(random.Number(0, maxLength - password.Length));
            }

            return new string(random.Shuffle(password).ToArray());
        }
    }
}
