using Domain.ProductManagement;
using MediatR;

namespace Application.ProductManagement.Queries.GetProducts
{
    public sealed class GetCategoriesListQuery : IRequest<List<Category>>
    {
    }
}
