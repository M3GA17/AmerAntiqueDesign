using Dapper;
using Domain.ProductManagement;
using Domain.ProductManagement.Repositories;
using Domain.ProductManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Shared.ValueObjects;

namespace Infrastructure.Database.Repository;
public class ProductRepository(ApplicationDbContext dbContext, IConfiguration configuration) : IProductRepository
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
        return await dbContext.Products.Include(p => p.ProductPhotos).FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
    public async Task<IEnumerable<Product>> GetListAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Products
            //.Include(p => p.IdCategory)
            .Include(p => p.ProductPhotos)
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
        string sql = "SELECT \"public\".\"get_next_serial_number\"()";

        await using var connection = new NpgsqlConnection(configuration.GetConnectionString("Default"));
        var count = await connection.ExecuteScalarAsync<int>(sql);
        return SerialNumber.Create(count.ToString());
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
