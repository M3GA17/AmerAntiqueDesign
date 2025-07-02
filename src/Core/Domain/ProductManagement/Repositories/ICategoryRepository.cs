using Domain.ProductManagement.ValueObjects;
using Shared.Base;

namespace Domain.ProductManagement.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category, IdCategory>
    {
        #region Category
        Task<bool> ExistsByNameAsync(string categoryName, CancellationToken cancellationToken);
        #endregion Category
    }
}
