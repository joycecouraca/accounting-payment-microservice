using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using AccountingPayment.Domain.Util.Calculator;
using AccountingPayment.Domain.Util.Constants;
using AccountingPayment.Domain.Util.Enum;
using AccountingPayment.Domain.Util.Extension;
using FluentValidation;
using Mapster;
using MediatR;

namespace AccountingPayment.Application.UserCases.PaycheckExtract.Command
{
    public class PaycheckExtractCommandHandler : IRequestHandler<PaycheckExtractRequest, ApplicationResult<PaycheckExtractResponse>>
    {
        private IEmployeeRepository<EmployeeEntity> _repositoryEmployee;
        private readonly IValidator<PaycheckExtractRequest> _validator;

        public PaycheckExtractCommandHandler(IEmployeeRepository<EmployeeEntity> repositoryEmployee, IValidator<PaycheckExtractRequest> validator)
        {
            _repositoryEmployee = repositoryEmployee;
            _validator = validator;
        }

        public async Task<ApplicationResult<PaycheckExtractResponse>> Handle(PaycheckExtractRequest request, CancellationToken cancellationToken)
        {
            var resultValidation = await _validator.ValidateAsync(request);

            if (!resultValidation.IsValid)
            {
                var res = resultValidation.Errors.Select(c => new ApplicationError(c.ErrorCode, c.ErrorMessage)).ToList();
                return new ApplicationResult<PaycheckExtractResponse>().ReponseErrorFluentValidator(res);
            }


            var employeeEntity = await _repositoryEmployee.SelectAsync(request.EmployeeId);


            return new ApplicationResult<PaycheckExtractResponse>().ReponseSuccess(GetPaycheckExtract(employeeEntity, request));
        }

        private static PaycheckExtractResponse GetPaycheckExtract(EmployeeEntity employeeEntity, PaycheckExtractRequest request)
        {
            var listRelease = new List<Release>()
            {
                NewRelease(ETypeDescriptionReleaseEnum.GrossSalary, ETypeReleaseEnum.Remuneration, (decimal)employeeEntity.GrossSalary),
                NewRelease(ETypeDescriptionReleaseEnum.Inss, ETypeReleaseEnum.Discount, CalculateDiscountInss((decimal)employeeEntity.GrossSalary)),
                NewRelease(ETypeDescriptionReleaseEnum.Irrf, ETypeReleaseEnum.Discount, CalculateDiscountIrpf((decimal)employeeEntity.GrossSalary)),
                NewRelease(ETypeDescriptionReleaseEnum.Fgts, ETypeReleaseEnum.Discount, CalculateDiscountFgts((decimal)employeeEntity.GrossSalary)),
                NewRelease(ETypeDescriptionReleaseEnum.HealthPlan, ETypeReleaseEnum.Discount, employeeEntity.HealthPlanDiscount ? PaycheckExtractConstants.DiscountHealthPlan : 0),
                NewRelease(ETypeDescriptionReleaseEnum.DentalPlan, ETypeReleaseEnum.Discount, employeeEntity.DentalPlanDiscount ? PaycheckExtractConstants.DiscountDentalPlan : 0),
                NewRelease(ETypeDescriptionReleaseEnum.TransportationVouchers, ETypeReleaseEnum.Discount, CalculateDiscountTransportationVoucher((decimal)employeeEntity.GrossSalary)),
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

        private static Release NewRelease(ETypeDescriptionReleaseEnum descriptionReleaseEnum, ETypeReleaseEnum eTypeReleaseEnum, decimal value)
        {
            return new Release
            {
                Description = descriptionReleaseEnum.GetEnumMemberValue(),
                Type = eTypeReleaseEnum.GetEnumMemberValue(),
                Value = value
            };
        }

        private static decimal CalculateDiscountTransportationVoucher(decimal grossSalary) => grossSalary >= PaycheckExtractConstants.MinimumSalaryDiscountTransportationVoucher ?
                                                                                              grossSalary * PaycheckExtractConstants.DiscountTransportationVoucher : 0;
        private static decimal CalculateDiscountFgts(decimal grossSalary) => grossSalary * PaycheckExtractConstants.DiscountFgts;

        private static decimal CalculateDiscountIrpf(decimal grossSalary)
        {
            return IRPFCalculator.GetIrpfRange(grossSalary);
        }

        private static decimal CalculateDiscountInss(decimal grossSalary)
        {
            return grossSalary * InssCalculator.GetInssRange(grossSalary);
        }
    }
}
