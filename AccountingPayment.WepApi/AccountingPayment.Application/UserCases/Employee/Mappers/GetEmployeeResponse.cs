using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Dtos.Sector.Response;
using AccountingPayment.Domain.Entities;
using Mapster;

namespace AccountingPayment.Application.UserCases.Employee.Mappers
{
    public static class GetEmployeeResponse
    {
        public static EmployeeResponse EmployeeToDto(this BaseEntity entity, SectorEntity sectorEntity)
        {
            var response = entity.Adapt<EmployeeResponse>();
            response.Sector = sectorEntity.Adapt<SectorResponse>();

            return response;
        }
    }
}
