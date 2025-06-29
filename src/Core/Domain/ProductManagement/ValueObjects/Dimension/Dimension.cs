using Shared.Base;

namespace Domain.ProductManagement.ValueObjects;
public sealed record Dimension
{
    public int Height { get; set; }
    public int Width { get; set; }
    public int Depth { get; set; }
    public bool IsBulky { get; set; }

    //extra
    //public string? Desc1 { get; set; }
    //public int? Dim1 { get; set; }
    //public string? Desc2 { get; set; }
    //public int? Dim2 { get; set; }

    private Dimension(int height, int width, int depth, bool isBulky)
    {
        Height = height; Width = width; Depth = depth; IsBulky = isBulky;
        //Desc1 = desc1; Dim1 = dim1; Desc2 = desc2; Dim2 = dim2;
    }

    public static Result<Dimension> Create(int height, int width, int depth, bool isBulky)
    {
        if (height == 0 || width == 0 || depth == 0)
        {
            return Result.Failure<Dimension>(DimensionErrors.DimensionCannotBeZero);
        }

        return new Dimension(height, width, depth, isBulky);
    }
}
