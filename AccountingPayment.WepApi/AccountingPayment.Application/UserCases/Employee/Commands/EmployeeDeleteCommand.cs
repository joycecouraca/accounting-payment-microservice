using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Interfaces.Repository;
using MediatR;

namespace AccountingPayment.Application.UserCases.Employee.Commands
{
    public record EmployeeDeleteCommand(Guid employeeId) : IRequest<ApplicationResult<bool>>;

    public class EmployeeDeleteCommandHandler : IRequestHandler<EmployeeDeleteCommand, ApplicationResult<bool>>
    {
        private IRepository<EmployeeEntity> _repository;
        public EmployeeDeleteCommandHandler(IRepository<EmployeeEntity> repository)
        {
            _repository = repository;
        }

        public async Task<ApplicationResult<bool>> Handle(EmployeeDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = await _repository.DeleteAsync(request.employeeId);

            return new ApplicationResult<bool>().ReponseSuccess(result);
        }
    }
}
