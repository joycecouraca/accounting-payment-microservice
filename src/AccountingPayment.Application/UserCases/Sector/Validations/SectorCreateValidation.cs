using AccountingPayment.Domain.Dtos.Sector.Request;
using FluentValidation;

namespace AccountingPayment.Application.UserCases.Sector.Validations
{
    public class SectorCreateValidation : AbstractValidator<SectorCreateRequest>
    {
        public SectorCreateValidation()
        {
            RuleFor(x => x.Name)
             .NotEmpty().WithMessage("The Name field is required.")
             .MaximumLength(50).WithMessage("The Name field must not exceed 50 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(100).WithMessage("The Description field must not exceed 100 characters.");
        }
    }
}
