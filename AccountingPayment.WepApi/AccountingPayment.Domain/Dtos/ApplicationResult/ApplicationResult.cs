using System.Runtime.Serialization;

namespace AccountingPayment.Domain.Dtos.ApplicationResult
{
    public class ApplicationResult<T>
    {
        public T? Data { get; set; }

        public bool Success
        {
            get
            {
                if (Data == null)
                {
                    return false;
                }

                if (Data != null && Errors!.Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public IEnumerable<ApplicationError>? Errors { get; set; }

        public virtual ApplicationResult<T> ReponseSuccess(T data)
        {
            Errors = new List<ApplicationError>();
            Data = data;
            return this;
        }
        public virtual ApplicationResult<T> ReponseError(string error, string code)
        {
            Errors = new List<ApplicationError>() { new ApplicationError(code, error) };
            return this;
        }
        public virtual ApplicationResult<T> ReponseErrorFluentValidator(List<ApplicationError> applicationErrors)
        {
            Errors = applicationErrors;
            return this;
        }
    }
}
