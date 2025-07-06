using Application.Abstractions.Context;
using Application.Abstractions.Messaging;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement;
using Shared.Base;
using Shared.Base.Validation;

namespace Application.ProductManagement.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IContextService contextService) : ICommandHandler<CreateCategoryCommand, Result>
    {
        public async Task<Result> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var we = contextService.IdUser;

            if (await categoryRepository.ExistsByNameAsync(command.Name, cancellationToken))
                throw new DomainException(DomainExceptionCode.Error_Category_Already_Exist);

            var idUser = new IdUser(new Guid("538e849e-a65b-4db3-8d79-1e8180284bbe"));
            var datetimeOffset = DateTimeOffset.UtcNow;

            IdCategory? idCategoryParent = command.IdCategoryParent.HasValue ? new IdCategory(command.IdCategoryParent.Value) : null;
            Category? categoryParent = await categoryRepository.GetAsync(idCategoryParent, cancellationToken);

            var newCategory = Category.Create(command.Name, command.Description, categoryParent, idUser, datetimeOffset);
            await categoryRepository.AddAsync(newCategory, cancellationToken);

            return Result.Success();
        }
    }
}
