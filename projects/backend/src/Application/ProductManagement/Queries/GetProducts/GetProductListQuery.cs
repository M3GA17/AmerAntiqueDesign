using Application.Abstractions;
using static Application.ProductManagement.Queries.GetProducts.GetProductListQuery;

namespace Application.ProductManagement.Queries.GetProducts
{
    public sealed class GetProductListQuery : IQuery<List<ProductResponse>>
    {
        public sealed class ProductResponse
        {
            public Guid Id { get; init; }
            //public string SerialNumber { get; init; } = string.Empty;
            public string Name { get; init; } = string.Empty;
            public string? Description { get; init; }
        }
    }
}
