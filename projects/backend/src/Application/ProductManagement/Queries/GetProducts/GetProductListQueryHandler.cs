using Application.Abstractions;
using Domain.ProductManagement.Repositories;
using static Application.ProductManagement.Queries.GetProducts.GetProductListQuery;

namespace Application.ProductManagement.Queries.GetProducts
{
    internal class GetProductListQueryHandler(IProductRepository productRepository) : IQueryHandler<GetProductListQuery, List<ProductResponse>>
    {
        public async Task<List<ProductResponse>> Handle(GetProductListQuery request, CancellationToken cancellationToken)
        {
            //return (await productRepository.GetListAsync(cancellationToken)).ToList();
            var products = await productRepository.GetListAsync(cancellationToken);


            return products.Select(p => new ProductResponse
            {
                Id = p.Id.Value,
                //SerialNumber = p.SerialNumber.Value,
                Name = p.Name,
                Description = p.Description
            }).ToList();
        }
    }
}
