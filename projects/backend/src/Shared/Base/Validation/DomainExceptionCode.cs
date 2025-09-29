namespace Shared.Base.Validation
{
    public enum DomainExceptionCode
    {
        //Generic domain errors
        Domain_Unexpected_Error,

        //Product errors
        Error_Product_Name_Required,
        Error_Dimension_Cannot_Be_Zero,

        //Category errors
        Error_Category_Already_Exist,
    }
}