
using Application.Abstractions.Messaging;
using Shared.Base.Validation;

namespace Application.ProductManagement.Commands.CreateCategory
{
    public class CreateCategoryCommand() : ICommand<Result>
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public Guid? IdCategoryParent { get; set; }

    }
}
