using AccountingPayment.Domain.Entities;
using AccountingPayment.Domain.Model.Employee;
using AutoMapper;

namespace AccountingPayment.CrossCutting.Mappings.Employee
{
    public class EmployeeModelToEntityProfile : Profile
    {
        public EmployeeModelToEntityProfile()
        {
            CreateMap<EmployeeModel, EmployeeEntity>()
               .ReverseMap();
        }
    }
}
