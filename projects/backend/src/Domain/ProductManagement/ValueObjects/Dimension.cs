using Shared.Base;
using Shared.Base.Validation;

namespace Domain.ProductManagement.ValueObjects;
public sealed record Dimension
{
    public int Height { get; set; }
    public int Width { get; set; }
    public int Depth { get; set; }
    public bool IsBulky { get; set; }
    public Dimension(int height, int width, int depth, bool isBulky)
    {
        if (height == 0 || width == 0 || depth == 0)
        {
            throw new DomainException(DomainExceptionCode.Error_Dimension_Cannot_Be_Zero);
        }

        Height = height; Width = width; Depth = depth; IsBulky = isBulky;
    }
}
