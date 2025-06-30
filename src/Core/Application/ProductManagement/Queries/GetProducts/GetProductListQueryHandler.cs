using Application.Abstractions;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;

namespace Application.ProductManagement.Queries.GetProducts
{
    internal class GetProductListQueryHandler : IQueryHandler<GetProductListQuery, List<Product>>
    {
        private readonly IProductRepository productRepository;
        public GetProductListQueryHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            return (await productRepository.GetListAsync(cancellationToken)).ToList();
        }
    }
}
