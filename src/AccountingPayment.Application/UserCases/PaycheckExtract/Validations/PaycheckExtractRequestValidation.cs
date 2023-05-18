using AccountingPayment.Domain.Dtos.Employee.Request;
using FluentValidation;

namespace AccountingPayment.Application.UserCases.PaycheckExtract.Validations
{
    public class PaycheckExtractRequestValidator : AbstractValidator<PaycheckExtractRequest>
    {
        public PaycheckExtractRequestValidator()
        {
            RuleFor(request => request.EmployeeId)
                .NotEmpty().WithMessage("EmployeeId is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid EmployeeId.");

            RuleFor(request => request.MonthReference)
                .NotEmpty().WithMessage("MonthReference is required.")
                .Matches(@"^\d{2}-\d{4}$").WithMessage("Invalid MonthReference format. The format should be 'MM-YYYY'.");
        }
    }
}
