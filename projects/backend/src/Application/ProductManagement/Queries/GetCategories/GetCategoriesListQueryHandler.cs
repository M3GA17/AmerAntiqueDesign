using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using MediatR;

namespace Application.ProductManagement.Queries.GetProducts;

internal class GetCategoriesListQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesListQuery, List<Category>>
{
    private readonly ICategoryRepository categoryRepository = categoryRepository;
    public async Task<List<Category>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
    {
        return (await categoryRepository.GetListAsync(cancellationToken)).ToList();
    }
}
