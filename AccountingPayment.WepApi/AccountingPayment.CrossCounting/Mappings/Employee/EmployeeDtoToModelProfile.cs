using AccountingPayment.Domain.Dtos.Employee.Request;
using AccountingPayment.Domain.Model.Employee;
using AutoMapper;

namespace AccountingPayment.CrossCutting.Mappings.Employee
{
    public class EmployeeDtoToModelProfile : Profile
    {
        public EmployeeDtoToModelProfile()
        {
            CreateMap<EmployeeModel, EmployeeCreateRequest>()
                .ReverseMap();
            CreateMap<EmployeeModel, EmployeeUpdateRequest>()
                .ReverseMap();
        }
    }
}
