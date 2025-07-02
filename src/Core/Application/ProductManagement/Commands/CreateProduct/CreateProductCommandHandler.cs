using Application.Abstractions.Messaging;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement.ValueObjects;
using Shared.Base;
using Shared.Base.Validation;

namespace Application.ProductManagement.Commands.CreateProduct;

public class CreateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand, Result>
{
    public async Task<Result> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(command.Name))
            throw new DomainException(DomainExceptionCode.Error_Product_Name_Required);

        var serialNumber = await productRepository.GetNextSerialNumberAsync(cancellationToken);
        var productStatus = ProductStatus.GetById(new IdProductStatus(command.IdProductStatus));
        var dimension = new Dimension(command.Height, command.Width, command.Depth, command.IsBulky);

        var idUser = new IdUser(new Guid("538e849e-a65b-4db3-8d79-1e8180284bbe"));
        var datetimeOffset = DateTimeOffset.UtcNow;

        var product = Product.Create(serialNumber, command.Name, command.Description, new IdCategory(command.IdCategory), productStatus, dimension, idUser, datetimeOffset);
        await productRepository.AddAsync(product, cancellationToken);

        return Result.Success();
    }
}
