using AccountingPayment.Domain.Entities;

namespace AccountingPayment.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeEntity>> SelectAsync();
        Task<EmployeeEntity> SelectAsync(Guid id);
    }
}
