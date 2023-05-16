using System.Runtime.Serialization;

namespace AccountingPayment.Domain.Dtos.ApplicationResult
{
    public class ApplicationError
    {
        public string? Code { get; set; }

        public string? Description { get; set; }

        public ApplicationError()
        {

        }
        public ApplicationError(string code, string description)
        {
            Code = code;
            Description = description;
        }
    }
}
