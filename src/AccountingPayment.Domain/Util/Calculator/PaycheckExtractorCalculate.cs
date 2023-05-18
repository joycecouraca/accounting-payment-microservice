using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Util.Constants;
using AccountingPayment.Domain.Util.Enum;
using AccountingPayment.Domain.Util.Extension;
using Mapster;

namespace AccountingPayment.Domain.Util.Calculator
{
    public class PaycheckExtractorCalculate
    {
        public static Release NewRelease(ETypeDescriptionReleaseEnum descriptionReleaseEnum, ETypeReleaseEnum eTypeReleaseEnum, decimal value)
        {
            return new Release
            {
                Description = descriptionReleaseEnum.GetEnumMemberValue(),
                Type = eTypeReleaseEnum.GetEnumMemberValue(),
                Value = value
            };
        }
        public static PaycheckExtractResponse GetPaycheckExtract(EmployeeEntity employeeEntity, PaycheckExtractRequest request)
        {
            var listRelease = new List<Release>()
            {
                NewRelease(ETypeDescriptionReleaseEnum.GrossSalary, ETypeReleaseEnum.Remuneration, (decimal)employeeEntity.GrossSalary),
                NewRelease(ETypeDescriptionReleaseEnum.Inss, ETypeReleaseEnum.Discount, PaycheckExtractorCalculate.CalculateDiscountInss((decimal)employeeEntity.GrossSalary)),
                NewRelease(ETypeDescriptionReleaseEnum.Irrf, ETypeReleaseEnum.Discount, PaycheckExtractorCalculate.CalculateDiscountIrpf((decimal)employeeEntity.GrossSalary)),
                NewRelease(ETypeDescriptionReleaseEnum.Fgts, ETypeReleaseEnum.Discount, PaycheckExtractorCalculate.CalculateDiscountFgts((decimal)employeeEntity.GrossSalary)),
                NewRelease(ETypeDescriptionReleaseEnum.HealthPlan, ETypeReleaseEnum.Discount, employeeEntity.HealthPlanDiscount ? PaycheckExtractConstants.DiscountHealthPlan : 0),
                NewRelease(ETypeDescriptionReleaseEnum.DentalPlan, ETypeReleaseEnum.Discount, employeeEntity.DentalPlanDiscount ? PaycheckExtractConstants.DiscountDentalPlan : 0),
                NewRelease(ETypeDescriptionReleaseEnum.TransportationVouchers, ETypeReleaseEnum.Discount, PaycheckExtractorCalculate.CalculateDiscountTransportationVoucher((decimal)employeeEntity.GrossSalary)),
            };

            var totalDiscount = listRelease.Where(c => c.Type.Equals(ETypeReleaseEnum.Discount.GetEnumMemberValue())).Select(c => c.Value).Sum();

            return new PaycheckExtractResponse
            {
                Employee = employeeEntity.Adapt<EmployeeResponse>(),
                MonthReference = request.MonthReference,
                TotalDiscounts = totalDiscount,
                NetSalary = (decimal)employeeEntity.GrossSalary - totalDiscount,
                Releases = listRelease
            };
        }

        public static decimal CalculateDiscountTransportationVoucher(decimal grossSalary) => grossSalary >= PaycheckExtractConstants.MinimumSalaryDiscountTransportationVoucher ?
                                                                                              grossSalary * PaycheckExtractConstants.DiscountTransportationVoucher : 0;
        public static decimal CalculateDiscountFgts(decimal grossSalary) => grossSalary * PaycheckExtractConstants.DiscountFgts;

        public static decimal CalculateDiscountIrpf(decimal grossSalary) => IRPFCalculator.GetIrpfRange(grossSalary);

        public static decimal CalculateDiscountInss(decimal grossSalary) => grossSalary * InssCalculator.GetInssRange(grossSalary);

    }
}
