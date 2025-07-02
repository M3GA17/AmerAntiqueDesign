using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Shared.ValueObjects;

namespace Infrastructure.Database.Repository;
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
            //.Include(p => p.Category)
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

    // get next serial number for a product
    public async Task<SerialNumber> GetNextSerialNumberAsync(CancellationToken cancellationToken)
    {
        // Get the maximum serial number value from the database
        var maxSerialNumberValue = await dbContext.Products
            .Select(p => p.SerialNumber.Value)
            .MaxAsync(cancellationToken);

        // Parse the maximum serial number value to an integer, defaulting to 0 if parsing fails
        var maxSerialNumber = int.TryParse(maxSerialNumberValue, out var parsedValue) ? parsedValue : 0;

        // Return the next serial number
        return SerialNumber.Create((maxSerialNumber + 1).ToString());
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
    public async Task<Category?> GetCategoryByIdAsync(IdCategory idCategory, CancellationToken cancellationToken)
    {
        return await dbContext.Categories
                    .Include(c => c.CategoryParent) // Include il genitore
                    .Include(c => c.SubCategories)  // Include le sottocategorie
                    .FirstOrDefaultAsync(c => c.Id == idCategory, cancellationToken);
    }
    public async Task<bool> CategoryExistsByNameAsync(string categoryName, CancellationToken cancellationToken)
    {
        return await dbContext.Categories.AnyAsync(x => x.Name == categoryName, cancellationToken);
    }
    #endregion Category

}
