using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Entities;
using AutoMapper;

namespace AccountingPayment.CrossCutting.Mappings.Employee
{
    public class EmployeeEntityToDtoProfile : Profile
    {
        public EmployeeEntityToDtoProfile()
        {
            CreateMap<EmployeeEntity, EmployeeResponse>()
               .ReverseMap();
        }
    }
}
