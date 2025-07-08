namespace Shared.Base.Interfaces;

public interface IBaseException
{
    string Code { get; }
    object[] AdditionalInfo { get; }
}
