namespace Shared.Base.Validation;

public enum ValidationExceptionCode
{
    //Generic validation errors
    Validation_Unexpected_Error,

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
