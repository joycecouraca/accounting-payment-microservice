using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Util.Extension;
using FluentValidation;

namespace AccountingPayment.Application.UserCases.Employee.Validations
{
    public class EmployeeCreateValidation : AbstractValidator<EmployeeCreateRequest>
    {
        public EmployeeCreateValidation()
        {
            RuleFor(e => e.Name)
            .NotEmpty().WithMessage("Name is required.");

            RuleFor(e => e.LastName)
                .NotEmpty().WithMessage("Last name is required.");

            RuleFor(e => e.Document)
                .NotEmpty().WithMessage("Document is required.")
                .Must(BeValidCPF).WithMessage("Invalid CPF format.");

            RuleFor(e => e.SectorId)
                .NotEmpty().WithMessage("SectorId is required.")
                .NotEqual(Guid.Empty).WithMessage("Invalid sector ID.");

            RuleFor(e => e.GrossSalary)
                .NotEmpty().WithMessage("Gross salary is required.")
                .GreaterThan(0).WithMessage("Gross salary must be greater than zero.");

            RuleFor(e => e.AdmissionDate)
                .NotEmpty().WithMessage("Admission date is required.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Admission date cannot be in the future.");
        }
        private bool BeValidCPF(string cpf)
        {
            return cpf.ValidateCPF();
        }
    }
}
