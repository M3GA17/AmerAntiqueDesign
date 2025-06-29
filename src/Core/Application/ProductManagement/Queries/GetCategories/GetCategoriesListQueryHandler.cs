using Application.Abstractions;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Shared.Base;

namespace Application.ProductManagement.Queries.GetProducts
{
    internal class GetCategoriesListQueryHandler : IQueryHandler<GetCategoriesListQuery, List<Category>>
    {
        private readonly IProductRepository productRepository;
        public GetCategoriesListQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<Result<List<Category>>> Handle(GetCategoriesListQuery request, CancellationToken cancellationToken)
        {
            return Result.Success((await productRepository.GetCategoriesAsync(cancellationToken)).ToList());
        }
    }
}
