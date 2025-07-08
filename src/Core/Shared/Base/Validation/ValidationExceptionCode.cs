namespace Shared.Base.Validation;

public enum ValidationExceptionCode
{
    //Generic validation errors
    ValidationUnexpectedError,

    #region ProductManagement

    //Product

    //SerialNumber
    ErrorSerialNumberCannotBeNull,
    ErrorSerialNumberInvalidLength,

    //Category
    #endregion ProductManagement


    ErrorProductNameCannotBeNull,
    ErrorProductNameTooLong
}
