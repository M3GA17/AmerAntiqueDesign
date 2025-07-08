
using Shared.Base.Interfaces;
using Shared.Base.Validation;

namespace Shared.Base
{
    public class DomainException : Exception, IBaseException
    {
        public string Code => ExceptionCode?.ToString() ?? string.Empty;
        public object[] AdditionalInfo { get; private set; } = [];
        public DomainExceptionCode? ExceptionCode { get; private set; }

        public DomainException(DomainExceptionCode code, params object[] parameters) : this(code.ToString(), code, parameters) { }
        public DomainException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
        public DomainException(string message, DomainExceptionCode code, params object[] additionalInfo) { }
        public DomainException(string errorMessage) : base(errorMessage) { }
        public DomainException() { }
    }

}
