using Shared.Base.Validation;

namespace Shared.ValueObjects;

public sealed record SerialNumber
{
    public const int MaxLength = 7;

    public string Value { get; }
    private SerialNumber(string value) => Value = value;


    public static SerialNumber Create(string serialNumber)
    {
        var validate = new ValidationExceptionCollection();

        if (string.IsNullOrEmpty(serialNumber))
        {
            throw new ValidationException(ValidationExceptionCode.ErrorSerialNumberCannotBeNull);
        }
        if (serialNumber.Length > MaxLength)
        {
            throw new ValidationException(ValidationExceptionCode.ErrorSerialNumberInvalidLength);
        }

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
