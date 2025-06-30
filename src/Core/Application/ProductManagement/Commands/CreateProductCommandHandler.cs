using Application.Abstractions.Messaging;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement.ValueObjects;
using MediatR;
using Shared.ValueObjects;

namespace Application.ProductManagement.Commands;

public class CreateProductCommandHandler(IProductRepository productRepository) : ICommandHandler<CreateProductCommand>
{
    public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var serialNumber = SerialNumber.Create("0000003");
        var productStatus = ProductStatus.GetById(new IdProductStatus(request.IdProductStatus));
        var dimension = Dimension.Create(request.Height, request.Width, request.Depth, request.IsBulky);

        var idUser = new IdUser(new Guid("538e849e-a65b-4db3-8d79-1e8180284bbe"));
        var datetimeOffset = DateTimeOffset.Now;

        var product = Product.Create(serialNumber, request.Name, request.Description, new IdCategory(request.IdCategory), productStatus, dimension, idUser, datetimeOffset);
        await productRepository.AddAsync(product, cancellationToken);

        return Unit.Value;
    }
}
