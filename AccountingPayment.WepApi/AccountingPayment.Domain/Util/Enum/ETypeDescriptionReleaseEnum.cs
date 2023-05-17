using System.Runtime.Serialization;

namespace AccountingPayment.Domain.Util.Enum
{
    public enum ETypeDescriptionReleaseEnum
    {
        [EnumMember(Value = "TransportationVouchers")]
        TransportationVouchers,
        [EnumMember(Value = "DentalPlan")]
        DentalPlan,
        [EnumMember(Value = "HealthPlan")]
        HealthPlan,
        [EnumMember(Value = "INSS")]
        Inss,
        [EnumMember(Value = "IRRF")]
        Irrf,
        [EnumMember(Value = "FGTS")]
        Fgts,
        [EnumMember(Value = "Gross Salary")]
        GrossSalary
    }
}
