using Application.Abstractions.Messaging;
using Shared.Base.Validation;

namespace Application.ProductManagement.Commands.CreateProduct
{
    public class CreateProductCommand : ICommand<Result>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid IdCategory { get; set; }
        public string IdProductStatus { get; set; } = null!;
        public int Height { get; set; }
        public int Width { get; set; }
        public int Depth { get; set; }
        public bool IsBulky { get; set; }
    }
}
