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
    }
}
