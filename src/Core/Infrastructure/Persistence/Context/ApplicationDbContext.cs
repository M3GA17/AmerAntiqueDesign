using Domain.ProductManagement;
using Domain.UserManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Context;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
       : IdentityDbContext<DomainUser, IdentityRole<Guid>, Guid>(options)
{
    //dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=AmerAntiqueDesign_Dev;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Persistence/Entities --context-dir Persistence/Context --context TuoDbContext --no-onconfiguring

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }

    #region Identity
    //public virtual DbSet<AspNetRole> AspNetRoles { get; set; }

    //public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; }

    //public virtual DbSet<DomainUser> AspNetUsers { get; set; }

    //public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }

    //public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }

    //public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; }
    #endregion Identity

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.HasPostgresExtension("uuid-ossp");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        const string identitySchema = "identity";
        modelBuilder.Entity<DomainUser>(b => b.ToTable("AspNetUsers", identitySchema));
        modelBuilder.Entity<IdentityRole<Guid>>(b => b.ToTable("AspNetRoles", identitySchema));
        modelBuilder.Entity<IdentityUserClaim<Guid>>(b => b.ToTable("AspNetUserClaims", identitySchema));
        modelBuilder.Entity<IdentityUserRole<Guid>>(b => b.ToTable("AspNetUserRoles", identitySchema));
        modelBuilder.Entity<IdentityUserLogin<Guid>>(b => b.ToTable("AspNetUserLogins", identitySchema));
        modelBuilder.Entity<IdentityRoleClaim<Guid>>(b => b.ToTable("AspNetRoleClaims", identitySchema));
        modelBuilder.Entity<IdentityUserToken<Guid>>(b => b.ToTable("AspNetUserTokens", identitySchema));

        //modelBuilder.Entity<Product>().HasKey(p => p.Id);
        //modelBuilder.Entity<Product>().Property(p => p.Id).HasConversion(id => id.Value, value => new IdProduct(value));

        //modelBuilder.Entity<Category>().HasKey(p => p.Id);
        //modelBuilder.Entity<Category>().Property(p => p.Id).HasConversion(id => id.Value, value => new IdCategory(value));

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}


