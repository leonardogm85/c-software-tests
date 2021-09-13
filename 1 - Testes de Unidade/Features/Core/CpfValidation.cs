using System.Linq;

namespace Features.Core
{
    public class CpfValidation
    {
        public bool EhValido(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return false;
            }

            var invalid = new string[]
            {
                "00000000000",
                "11111111111",
                "22222222222",
                "33333333333",
                "44444444444",
                "55555555555",
                "66666666666",
                "77777777777",
                "88888888888",
                "99999999999"
            };

            if (invalid.Any(i => i == cpf))
            {
                return false;
            }

            var add = 0;

            for (var i = 0; i < 9; i++)
            {
                add += int.Parse(cpf[i].ToString()) * (10 - i);
            }

            var rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
            {
                rev = 0;
            }

            if (rev != int.Parse(cpf[9].ToString()))
            {
                return false;
            }

            add = 0;

            for (var i = 0; i < 10; i++)
            {
                add += int.Parse(cpf[i].ToString()) * (11 - i);
            }

            rev = 11 - (add % 11);

            if (rev == 10 || rev == 11)
            {
                rev = 0;
            }

            if (rev != int.Parse(cpf[10].ToString()))
            {
                return false;
            }

            return true;
        }
    }
}
