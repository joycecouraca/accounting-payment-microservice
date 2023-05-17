using AccountingPayment.Domain.Util.Enum;

namespace AccountingPayment.Domain.Util.Calculator
{
    public static class InssCalculator
    {
        private static readonly Dictionary<EAliquotInss, decimal> InssAliquotas = new Dictionary<EAliquotInss, decimal>
        {
            { EAliquotInss.Range1, 0.075m },
            { EAliquotInss.Range2, 0.09m },
            { EAliquotInss.Range3, 0.12m },
            { EAliquotInss.Range4, 0.14m }
        };

        private static readonly Dictionary<ESalaryRangeInssEnum, decimal> SalaryRange = new Dictionary<ESalaryRangeInssEnum, decimal>
        {
            { ESalaryRangeInssEnum.SalaryRange1, 1045.00m },
            { ESalaryRangeInssEnum.SalaryRange2, 2089.60m },
            { ESalaryRangeInssEnum.SalaryRange3, 3134.40m },
            { ESalaryRangeInssEnum.SalaryRange4, 6101.06m }
        };

        public static decimal GetInssRange(decimal valor)
        {
            foreach (var faixa in SalaryRange.Keys)
            {
                var limiteSuperior = SalaryRange[faixa];
                if (valor <= limiteSuperior)
                {
                    return GetRangeAliquot(faixa);
                }
            }

            return InssAliquotas[EAliquotInss.Range4];
        }
        private static decimal GetRangeAliquot(ESalaryRangeInssEnum faixa)
        {
            switch (faixa)
            {
                case ESalaryRangeInssEnum.SalaryRange1:
                    return InssAliquotas[EAliquotInss.Range1];
                case ESalaryRangeInssEnum.SalaryRange2:
                    return InssAliquotas[EAliquotInss.Range2];
                case ESalaryRangeInssEnum.SalaryRange3:
                    return InssAliquotas[EAliquotInss.Range3];
                case ESalaryRangeInssEnum.SalaryRange4:
                    return InssAliquotas[EAliquotInss.Range4];
                default:
                    throw new ArgumentOutOfRangeException(nameof(faixa));
            }
        }
    }
}
