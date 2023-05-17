using System.Globalization;

namespace AccountingPayment.Domain.Util.Extension
{
    public static class StringExtension
    {
        public static bool ValidateCPF(this string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", "");

            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
                return false;

            // Check for repeated digits
            if (cpf.Distinct().Count() == 1)
                return false;

            // Calculate verifier digits
            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string cpfSemDigito = cpf.Substring(0, 9);

            int digito1 = CalculateDigitoVerificador(cpfSemDigito, multiplicadores1);
            cpfSemDigito += digito1;

            int digito2 = CalculateDigitoVerificador(cpfSemDigito, multiplicadores2);
            cpfSemDigito += digito2;

            // Check if the CPF is valid
            return cpf == cpfSemDigito;
        }

        private static int CalculateDigitoVerificador(string cpfSemDigito, int[] multiplicadores)
        {
            int soma = cpfSemDigito.Select((digito, indice) => int.Parse(digito.ToString()) * multiplicadores[indice]).Sum();
            int resto = soma % 11;

            return resto < 2 ? 0 : 11 - resto;
        }

        public static string? ToCamelCase(this string? str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            var cultureInfo = CultureInfo.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            var words = str.Split(' ', '-', '_', '.');
            var newWords = new List<string>();

            foreach (var word in words)
            {
                if (word.Length > 0)
                {
                    newWords.Add(char.ToUpperInvariant(word[0]) + word.Substring(1));
                }
            }

            return string.Join("", newWords);
        }
    }
}
