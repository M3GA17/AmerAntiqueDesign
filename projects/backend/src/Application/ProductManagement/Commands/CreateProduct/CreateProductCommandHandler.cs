using Application.Abstractions.Messaging;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
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
        var dimension = new Dimension(command.Height, command.Width, command.Depth, command.IsBulky);

        var product = Product.Create(serialNumber, command.Name, command.Description, dimension);
        await productRepository.AddAsync(product, cancellationToken);

        return Result.Success();
    }
}
