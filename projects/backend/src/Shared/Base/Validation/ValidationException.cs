using Shared.Base.Interfaces;

namespace Shared.Base.Validation;

public class ValidationException : Exception, IBaseException
{
    public string Code { get; }
    public object[] AdditionalInfo { get; }
    public ValidationExceptionCode ValidationErrorCode { get; }

    public ValidationException(ValidationExceptionCode code, params object[] additionalInfo)
        : base(code.ToString())
    {
        ValidationErrorCode = code;
        Code = code.ToString();
        AdditionalInfo = additionalInfo ?? [];
    }

    public ValidationException(string message, ValidationExceptionCode code, params object[] additionalInfo)
        : base(message)
    {
        ValidationErrorCode = code;
        Code = code.ToString();
        AdditionalInfo = additionalInfo ?? [];
    }

    public ValidationException(string message, Exception innerException, ValidationExceptionCode code, params object[] additionalInfo)
        : base(message, innerException)
    {
        ValidationErrorCode = code;
        Code = code.ToString();
        AdditionalInfo = additionalInfo ?? [];
    }
}

public class ValidationExceptionCollection : Exception
{
    public IDictionary<string, ICollection<IBaseException>> Errors { get; } = new Dictionary<string, ICollection<IBaseException>>();
    public bool HasErrors => Errors.Any();

    public ValidationExceptionCollection() { }

    public ValidationExceptionCollection(string subject, ValidationExceptionCode code, params object[] additionalInfo)
        : this(subject, new ValidationException(code, additionalInfo)) { }

    public ValidationExceptionCollection(string subject, params IBaseException[] errors)
    {
        foreach (var error in errors) AddError(subject, error);
    }

    public void TryThrow()
    {
        if (HasErrors) throw this;
    }

    public void AddError(string subject, ValidationExceptionCode code, params object[] additionalInfo)
        => AddError(subject, new ValidationException(code, additionalInfo));

    private void AddError(string subject, IBaseException error)
    {
        if (Errors.TryGetValue(subject, out var list)) list.Add(error);
        else Errors[subject] = new List<IBaseException> { error };
    }
}