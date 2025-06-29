using Application.Abstractions;
using Domain.ProductManagement;

namespace Application.ProductManagement.Queries.GetProducts
{
    public sealed class GetCategoriesListQuery : IQuery<List<Category>>
    {
    }
}
