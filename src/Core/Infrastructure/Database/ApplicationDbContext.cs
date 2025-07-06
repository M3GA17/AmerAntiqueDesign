using Application.Abstractions.UnitOfWork;
using Domain.ProductManagement;
using Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Shared.Primitives.Interfaces;

namespace Infrastructure.Database;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    //dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=AmerAntiqueDesign_Dev;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Persistence/Entities --context-dir Persistence/Context --context TuoDbContext --no-onconfiguring

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql();
    //}

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("uuid-ossp");
        //modelBuilder.UseHiLo(); //Nel caso l'id non venga creato dal dominio ma dal database
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        IncrementDatabaseVersion();
        return await base.SaveChangesAsync(cancellationToken);
    }
    private void IncrementDatabaseVersion()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is IAggregateRoot aggregateRoot && entry.State == EntityState.Modified)
            {
                aggregateRoot.IncrementVersion();
            }
        }
    }
}


