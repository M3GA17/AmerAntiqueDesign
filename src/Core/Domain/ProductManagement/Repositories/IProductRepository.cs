using Domain.ProductManagement.ValueObjects;
using Shared.Base;

namespace Domain.ProductManagement.Repositories
{
    public interface IProductRepository : IBaseRepository<Product, IdProduct>
    {
        Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken);
        Task<Category?> GetCategoryByIdAsync(IdCategory idCategory, CancellationToken cancellationToken);

    }
}
