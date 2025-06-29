namespace Shared.ValueObjects;

public sealed record SerialNumber
{
    public const int MaxLength = 7;

    public string Value { get; }
    private SerialNumber(string value) => Value = value;


    public static SerialNumber Create(string serialNumber)
    {
        return new SerialNumber(serialNumber.PadLeft(MaxLength, '0'));
    }

    //public static Result<SerialNumber> Create(string serialNumber)
    //{
    //    if (string.IsNullOrEmpty(serialNumber))
    //    {
    //        return Result.Failure<SerialNumber>(SerialNumberErrors.Empty);
    //    }
    //    if (serialNumber.Length > MaxLength)
    //    {
    //        return Result.Failure<SerialNumber>(SerialNumberErrors.MaxLength);
    //    }
    //    return new SerialNumber(serialNumber.PadLeft(MaxLength, '0'));
    //}
}
