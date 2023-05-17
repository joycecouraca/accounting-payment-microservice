using System.Runtime.Serialization;

namespace AccountingPayment.Domain.Util.Enum
{
    public enum ETypeReleaseEnum
    {
        [EnumMember(Value = "Discount")]
        Discount,
        [EnumMember(Value = "Remuneration")]
        Remuneration
    }
}
