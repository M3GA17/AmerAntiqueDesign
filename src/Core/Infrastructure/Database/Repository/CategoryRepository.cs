using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database;
public class CategoryRepository(ApplicationDbContext dbContext) : ICategoryRepository
{
    #region Category
    public async Task AddAsync(Category entity, CancellationToken cancellationToken)
    {
        await dbContext.Categories.AddAsync(entity, cancellationToken);
    }
    public async Task<bool> ExistsAsync(IdCategory id, CancellationToken cancellationToken)
    {
        return await dbContext.Categories.AnyAsync(p => p.Id == id, cancellationToken);
    }
    public async Task<Category?> GetAsync(IdCategory id, CancellationToken cancellationToken)
    {
        return await dbContext.Categories
            .Include(c => c.CategoryParent) // Include il genitore
            .Include(c => c.SubCategories)  // Include le sottocategorie
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<Category>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Categories
           .Include(c => c.CategoryParent)
           .Include(c => c.SubCategories)
           .AsNoTracking().ToListAsync(cancellationToken);
    }
    public async Task RemoveAsync(IdCategory id, CancellationToken cancellationToken)
    {
        var category = await dbContext.Categories
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

        if (category is not null)
        {
            dbContext.Categories.Remove(category);
        }
    }
    public async Task<bool> ExistsByNameAsync(string categoryName, CancellationToken cancellationToken)
    {
        return await dbContext.Categories.AnyAsync(x => x.Name == categoryName, cancellationToken);
    }
    #endregion Category

}
