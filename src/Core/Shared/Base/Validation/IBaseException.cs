namespace Shared.Base.Validation;

public interface IBaseException
{
    string Code { get; }
    object[] AdditionalInfo { get; }
}
