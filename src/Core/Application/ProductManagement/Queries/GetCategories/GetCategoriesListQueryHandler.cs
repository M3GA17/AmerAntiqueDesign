using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using MediatR;

namespace Application.ProductManagement.Queries.GetProducts;

internal class GetCategoriesListQueryHandler(IProductRepository productRepository) : IRequestHandler<GetCategoriesListQuery, List<Category>>
{
    private readonly IProductRepository productRepository = productRepository;
    public async Task<List<Category>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        return (await productRepository.GetCategoriesAsync(cancellationToken)).ToList();
    }
}
