using AccountingPayment.Domain.Dtos.Sector.Request;
using FluentValidation;

namespace AccountingPayment.Application.UserCases.Sector.Validations
{
    public class SectorUpdateValidation : AbstractValidator<SectorUpdateRequest>
    {
        public SectorUpdateValidation()
        {
            Include(new SectorCreateValidation());

            RuleFor(e => e.Id)
            .NotNull().WithMessage("Id is required");
        }
    }
}
