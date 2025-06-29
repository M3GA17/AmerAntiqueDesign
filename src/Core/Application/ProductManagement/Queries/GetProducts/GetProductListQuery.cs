using Application.Abstractions;
using Domain.ProductManagement;

namespace Application.ProductManagement.Queries.GetProducts
{
    public sealed class GetProductListQuery : IQuery<List<Product>>
    {
    }
}
