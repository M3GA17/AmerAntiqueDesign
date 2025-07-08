using Shared.Base.Interfaces;

namespace Shared.Base.Validation;

public class ValidationException : Exception, IBaseException
{

    public string Code => ValidationErrorCode?.ToString() ?? string.Empty;
    public object[] AdditionalInfo { get; private set; } = [];
    public ValidationExceptionCode? ValidationErrorCode { get; private set; }

    public ValidationException(ValidationExceptionCode code, params object[] parameters) : this(code.ToString(), code, parameters) { }
    public ValidationException(string errorMessage, Exception innerException) : base(errorMessage, innerException) { }
    public ValidationException(string message, ValidationExceptionCode code, params object[] additionalInfo) { }
    public ValidationException(string errorMessage) : base(errorMessage) { }
    public ValidationException() { }
}

public class ValidationExceptionCollection : Exception
{
    public IDictionary<string, ICollection<IBaseException>> Errors { get; set; }
            = new Dictionary<string, ICollection<IBaseException>>();
    public bool HasErrors => Errors.Any();

    public ValidationExceptionCollection()
    { }
    public ValidationExceptionCollection(string subject, ValidationExceptionCode code, params object[] additionalInfo)
            : this(subject, new ValidationException(code, additionalInfo))
    { }

    public ValidationExceptionCollection(string subject, params IBaseException[] errors)
    {
        foreach (var error in errors)
        {
            AddError(subject, error);
        }
    }

    public void AddError(string subject, IBaseException error)
    {
        if (Errors.ContainsKey(subject))
            Errors[subject].Add(error);
        else
            Errors.Add(subject, new List<IBaseException> { error });
    }
}
