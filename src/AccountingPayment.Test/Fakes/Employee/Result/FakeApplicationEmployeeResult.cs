using AccountingPayment.Domain.Dtos.ApplicationResult;
using AccountingPayment.Domain.Entities;
using Mapster;

namespace AccountingPayment.Test.Fakes.Employee.Result
{
    public static class FakeApplicationEmployeeResult<T> where T : class
    {
        public static ApplicationResult<T> GetResponseSuccess(EmployeeEntity employee)
        {
            return new ApplicationResult<T>().ReponseSuccess(employee.Adapt<T>());
        }
        public static ApplicationResult<T> GetResponseSuccessList(List<EmployeeEntity> employee)
        {
            return new ApplicationResult<T>().ReponseSuccess(employee.Adapt<T>());
        }
        public static ApplicationResult<T> GetResponseErrorNotFound()
        {
            return new ApplicationResult<T>().ReponseError("NotFound", "Employee not Found");
        }
    }
}
