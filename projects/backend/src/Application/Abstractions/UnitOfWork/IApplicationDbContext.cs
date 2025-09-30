using Domain.ProductManagement;
using Microsoft.EntityFrameworkCore;

namespace Application.Abstractions.UnitOfWork
{
    public interface IApplicationDbContext
    {
        DbSet<Product> Products { get; set; }
        //DbSet<Category> Categories { get; set; }

        //Task<int> SaveChangesAsync(CancellationToken cancellationToken = default); //per notificare domain event 
    }
}
