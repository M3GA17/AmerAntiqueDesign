using Shared.Base.Interfaces;
using Shared.Base.Validation;

namespace Shared.Base
{
    public class DomainException : Exception, IBaseException
    {
        public string Code { get; }
        public object[] AdditionalInfo { get; }
        public DomainExceptionCode ExceptionCode { get; }

        public DomainException(DomainExceptionCode code, params object[] additionalInfo)
            : base(code.ToString())
        {
            ExceptionCode = code;
            Code = code.ToString();
            AdditionalInfo = additionalInfo ?? [];
        }

        public DomainException(string message, DomainExceptionCode code, params object[] additionalInfo)
            : base(message)
        {
            ExceptionCode = code;
            Code = code.ToString();
            AdditionalInfo = additionalInfo ?? [];
        }

        public DomainException(string message, Exception innerException, DomainExceptionCode code, params object[] additionalInfo)
            : base(message, innerException)
        {
            ExceptionCode = code;
            Code = code.ToString();
            AdditionalInfo = additionalInfo ?? [];
        }
    }

}
