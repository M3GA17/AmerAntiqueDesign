using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class ProductRepository(ApplicationDbContext dbContext) : IProductRepository
    {
        #region Product
        public async Task AddAsync(Product entity, CancellationToken cancellationToken)
        {
            await dbContext.Products.AddAsync(entity, cancellationToken);
        }
        public async Task<bool> ExistsAsync(IdProduct id, CancellationToken cancellationToken)
        {
            return await dbContext.Products.AnyAsync(p => p.Id == id, cancellationToken);
        }

        public async Task<Product?> GetAsync(IdProduct id, CancellationToken cancellationToken)
        {
            return await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        }
        public async Task<IEnumerable<Product>> GetListAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Products
                .Include(p => p.Category)
                .AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task RemoveAsync(IdProduct id, CancellationToken cancellationToken)
        {
            var product = await dbContext.Products
                .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);

            if (product is not null)
            {
                dbContext.Products.Remove(product);
            }
        }
        #endregion Product

        #region Category
        public async Task<IEnumerable<Category>> GetCategoriesAsync(CancellationToken cancellationToken)
        {
            return await dbContext.Categories
                .Include(c => c.CategoryParent)
                .Include(c => c.SubCategories)
                .AsNoTracking().ToListAsync(cancellationToken);
        }
        public async Task<Category?> GetCategoryByIdAsync(IdCategory id)
        {
            // Supponiamo che '_context' sia un'istanza del tuo DbContext
            return await dbContext.Categories
                        .Include(c => c.CategoryParent) // Include il genitore
                        .Include(c => c.SubCategories)  // Include le sottocategorie
                        .FirstOrDefaultAsync(c => c.Id == id);
        }
        #endregion Category

    }
}
