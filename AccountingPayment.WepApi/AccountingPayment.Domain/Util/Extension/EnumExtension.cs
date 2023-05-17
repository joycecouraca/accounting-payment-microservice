using System.Runtime.Serialization;


namespace AccountingPayment.Domain.Util.Extension
{
    public static class EnumExtension
    {
        public static string GetEnumMemberValue<T>(this T value) where T : IConvertible
        {
            var memberAttribute = value.GetType()
                .GetMember(value.ToString())[0]
                .GetCustomAttributes(typeof(EnumMemberAttribute), false)
                .FirstOrDefault() as EnumMemberAttribute;

            return memberAttribute?.Value;
        }
        public static T GetEnum<T>(this string request) where T : IConvertible
        {
            return (T)System.Enum.Parse(typeof(T), request.ToCamelCase());
        }
    }
}
