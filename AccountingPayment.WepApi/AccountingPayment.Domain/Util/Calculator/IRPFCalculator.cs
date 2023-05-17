using AccountingPayment.Domain.Util.Enum;

namespace AccountingPayment.Domain.Util.Calculator
{
    public static class IRPFCalculator
    {
        private static readonly Dictionary<EAliquotIrpf, decimal> InssAliquotas = new Dictionary<EAliquotIrpf, decimal>
        {
            { EAliquotIrpf.Range1, 0.0m },
            { EAliquotIrpf.Range2, 142.80m },
            { EAliquotIrpf.Range3, 354.80m },
            { EAliquotIrpf.Range4, 636.13m },
            { EAliquotIrpf.Range5, 869.36m }
        };

        private static readonly Dictionary<ESalaryRangeIrpfEnum, decimal> SalaryRange = new Dictionary<ESalaryRangeIrpfEnum, decimal>
        {
            { ESalaryRangeIrpfEnum.SalaryRange1, 1903.98m },
            { ESalaryRangeIrpfEnum.SalaryRange2, 2826.65m },
            { ESalaryRangeIrpfEnum.SalaryRange3, 3751.05m },
            { ESalaryRangeIrpfEnum.SalaryRange4, 4664.68m }
        };

        public static decimal GetIrpfRange(decimal valor)
        {
            foreach (var faixa in SalaryRange.Keys)
            {
                var limiteSuperior = SalaryRange[faixa];
                if (valor <= limiteSuperior)
                {
                    return GetRangeAliquot(faixa);
                }
            }

            return InssAliquotas[EAliquotIrpf.Range5];
        }
        private static decimal GetRangeAliquot(ESalaryRangeIrpfEnum faixa)
        {
            switch (faixa)
            {
                case ESalaryRangeIrpfEnum.SalaryRange1:
                    return InssAliquotas[EAliquotIrpf.Range1];
                case ESalaryRangeIrpfEnum.SalaryRange2:
                    return InssAliquotas[EAliquotIrpf.Range2];
                case ESalaryRangeIrpfEnum.SalaryRange3:
                    return InssAliquotas[EAliquotIrpf.Range3];
                case ESalaryRangeIrpfEnum.SalaryRange4:
                    return InssAliquotas[EAliquotIrpf.Range4];
                default:
                    throw new ArgumentOutOfRangeException(nameof(faixa));
            }
        }
    }
}
