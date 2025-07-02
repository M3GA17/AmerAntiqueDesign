using FluentValidation;

namespace Application.ProductManagement.Commands.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    //public CreateCategoryCommandValidator()
    //{
    //    // Regola 1: Name non deve essere vuoto
    //    RuleFor(x => x.Name)
    //        .NotEmpty()
    //        .WithMessage(Error.Custom("Category.Name.Empty", "Category name cannot be empty.").Message) // Puoi usare il messaggio dal tuo Error VO
    //        .WithErrorCode(Error.Custom("Category.Name.Empty", "").Code); // E anche il codice
    //                                                                      // Regola 2: Name non deve superare i 100 caratteri
    //    RuleFor(x => x.Name)
    //        .MaximumLength(100)
    //        .WithMessage(Error.Custom("Category.Name.TooLong", "Category name cannot exceed 100 characters.").Message)
    //        .WithErrorCode(Error.Custom("Category.Name.TooLong", "").Code);
    //    // Regola 3: IdCategoryParent deve essere un GUID valido se presente
    //    When(x => x.IdCategoryParent.HasValue, () =>
    //    {
    //        RuleFor(x => x.IdCategoryParent)
    //            .Must(id => Guid.TryParse(id.ToString(), out _)) // Assicurati che sia un GUID valido
    //            .WithMessage(Error.Custom("Category.ParentId.InvalidFormat", "Parent category ID must be a valid GUID.").Message)
    //            .WithErrorCode(Error.Custom("Category.ParentId.InvalidFormat", "").Code);
    //    });
    //}
}