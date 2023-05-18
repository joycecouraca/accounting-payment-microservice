using AccountingPayment.Domain.Entities;

namespace AccountingPayment.Domain.Interfaces.Repository
{
    public interface IEmployeeRepository<T> : IRepository<T> where T : EmployeeEntity
    {
    }
}
