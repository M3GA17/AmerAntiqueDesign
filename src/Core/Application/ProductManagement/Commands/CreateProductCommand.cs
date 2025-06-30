using Application.Abstractions.Messaging;

namespace Application.ProductManagement.Commands
{
    public class CreateProductCommand : ICommand
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
