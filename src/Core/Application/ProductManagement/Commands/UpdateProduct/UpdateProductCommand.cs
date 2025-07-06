using Application.Abstractions.Messaging;
using Shared.Base.Validation;

namespace Application.ProductManagement.Commands.CreateProduct
{
    public class UpdateProductCommand : ICommand<Result>
    {
        public Guid IdProduct { get; set; }
        public string Name { get; set; } = null!;
    }
}
