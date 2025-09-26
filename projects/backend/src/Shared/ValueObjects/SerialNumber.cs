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
            validate.AddError(nameof(serialNumber), ValidationExceptionCode.ErrorSerialNumberCannotBeNull);
        if (serialNumber.Length > MaxLength)
            validate.AddError(nameof(serialNumber), ValidationExceptionCode.ErrorSerialNumberInvalidLength);

        validate.TryThrow();

        return new SerialNumber(serialNumber.PadLeft(MaxLength, '0'));
    }
}
