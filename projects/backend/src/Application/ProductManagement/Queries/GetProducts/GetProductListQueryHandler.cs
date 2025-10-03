using Application.Abstractions;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;

namespace Application.ProductManagement.Queries.GetProducts
{
    internal class GetProductListQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductListQuery, List<Product>>
    {
        public async Task<List<Product>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            throw new Exception("Errore di prova");
            return (await productRepository.GetListAsync(cancellationToken)).ToList();
        }
    }
}
