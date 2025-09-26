using Application.Abstractions.Messaging;
using Shared.Base.Validation;

namespace Application.ProductManagement.Commands.CreateProduct
{
    public class UpdatePhotoCommand : ICommand<Result>
    {
        public Guid IdProductPhoto { get; set; }
        public Guid IdProduct { get; set; }
        public string Name { get; set; } = null!;
    }
}
