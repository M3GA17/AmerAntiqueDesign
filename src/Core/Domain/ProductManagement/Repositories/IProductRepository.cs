using Domain.ProductManagement.ValueObjects;
using Shared.Base;

namespace Domain.ProductManagement.Repositories
{
    public interface IProductRepository : IBaseRepository<Product, IdProduct>
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);

    }
}
