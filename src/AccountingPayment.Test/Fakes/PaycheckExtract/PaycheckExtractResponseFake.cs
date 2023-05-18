using AccountingPayment.Domain.Dtos.Employee.Response;
using AccountingPayment.Domain.Dtos.Sector.Response;

namespace AccountingPayment.Test.Fakes.PaycheckExtract
{
    public static class PaycheckExtractResponseFake
    {
        public static PaycheckExtractResponse GenerateFake()
        {
            return new PaycheckExtractResponse
            {
                Employee = new EmployeeResponse
                {
                    Id = Guid.Parse("d1cb0bbf-e760-40f6-a856-4610d573bd98"),
                    Name = "Joyce",
                    LastName = "Couraça",
                    Document = "44812164842",
                    GrossSalary = 1000,
                    AdmissionDate = DateTime.Parse("2023-05-15T19:52:31.52"),
                    HealthPlanDiscount = true,
                    DentalPlanDiscount = true,
                    TransportationVoucherDiscount = true,
                    Sector = new SectorResponse
                    {
                        Id = Guid.Parse("6b6ec5d2-8f22-4a8a-9c39-19b66e4b8c01"),
                        Name = "Developer",
                        Description = "Developer"
                    }
                },
                MonthReference = "05-2023",
                NetSalary = (decimal)830.000,
                TotalDiscounts = (decimal)170.000,
                Releases = new List<Release>
            {
                new Release
                {
                    Type = "Remuneration",
                    Value = (decimal) 1000.000,
                    Description = "Gross Salary"
                },
                new Release
                {
                    Type = "Discount",
                    Value = (decimal) 75.000,
                    Description = "INSS"
                },
                new Release
                {
                    Type = "Discount",
                    Value = (decimal)0.0,
                    Description = "IRRF"
                },
                new Release
                {
                    Type = "Discount",
                    Value = (decimal)80.00,
                    Description = "FGTS"
                },
                new Release
                {
                    Type = "Discount",
                    Value = (decimal) 10.00,
                    Description = "HealthPlan"
                },
                new Release
                {
                    Type = "Discount",
                    Value = (decimal) 5.00,
                    Description = "DentalPlan"
                },
                new Release
                {
                    Type = "Discount",
                    Value = (decimal) 0.0,
                    Description = "TransportationVouchers"
                }
            }
            };
        }
    }
}
