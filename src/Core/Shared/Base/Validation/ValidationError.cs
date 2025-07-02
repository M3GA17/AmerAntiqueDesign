namespace Shared.Base.Validation
{
    public class ValidationError(ValidationErrorCode code, object[]? additionalInfo) : IBaseException
    {
        public string Code => ErrorCode.ToString();
        public ValidationErrorCode ErrorCode { get; } = code;
        public object[] AdditionalInfo { get; } = additionalInfo ?? [];
    }
}
