using AccountingPayment.Domain.Dtos.Employee.Request;
using FluentValidation;

namespace AccountingPayment.Application.UserCases.Employee.Validations
{
    public class EmployeeUpdateValidation : AbstractValidator<EmployeeUpdateRequest>
    {
        public EmployeeUpdateValidation()
        {
            Include(new EmployeeCreateValidation());

            RuleFor(e => e.Id)
            .NotNull().WithMessage("Id is required");
        }
    }
}
