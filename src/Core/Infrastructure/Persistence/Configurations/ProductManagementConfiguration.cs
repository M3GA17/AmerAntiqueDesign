using Domain.ProductManagement;
using Domain.ProductManagement.ValueObjects;
using Domain.UserManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects;

namespace Infrastructure.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("products_tb");

        builder.HasKey(p => p.Id).HasName("PK_products_tb_idProduct");

        builder.HasIndex(p => p.SerialNumber).IsUnique().HasDatabaseName("UI_products_tb_serialNumber");

        builder.Property(p => p.Id).HasColumnName("idProduct").IsRequired().HasConversion(id => id.Value, value => new IdProduct(value));
        builder.Property(p => p.SerialNumber).HasColumnName("serialNumber").IsRequired().HasConversion(sn => sn.Value, value => SerialNumber.Create(value)).HasMaxLength(7);
        builder.Property(p => p.Name).HasColumnName("name").IsRequired().HasMaxLength(512);
        builder.Property(p => p.Description).HasColumnName("description").HasMaxLength(2048);
        //builder.Property(p => p.IdCategory).HasColumnName("idCategory").IsRequired().HasConversion(id => id.Value, value => new IdCategory(value));
        builder.Property(p => p.ProductStatus).HasColumnName("idProductStatus").IsRequired()
               .HasConversion(status => status.Id.Value, value => ProductStatus.GetById(new IdProductStatus(value))).HasMaxLength(5).IsFixedLength();
        builder.OwnsOne(p => p.Dimension, dimensionBuilder =>
        {
            dimensionBuilder.Property(d => d.Height).HasColumnName("height").IsRequired();
            dimensionBuilder.Property(d => d.Width).HasColumnName("width").IsRequired();
            dimensionBuilder.Property(d => d.Depth).HasColumnName("depth").IsRequired();
            dimensionBuilder.Property(d => d.IsBulky).HasColumnName("isBulky").IsRequired();
        });
        builder.Property(p => p.IdUserCreate).HasColumnName("idUserCreate").IsRequired().HasConversion(id => id.Value, value => new IdUser(value));
        builder.Property(p => p.DateCreate).HasColumnName("dateCreate").IsRequired();
        builder.Property(p => p.IdUserUpdate).HasColumnName("idUserUpdate").IsRequired().HasConversion(id => id.Value, value => new IdUser(value));
        builder.Property(p => p.DateUpdate).HasColumnName("dateUpdate").IsRequired();
        builder.Property(p => p.DatabaseVersion).HasColumnName("databaseVersion").IsRequired();

        //builder.HasOne(p => p.ProductStatus)
        //   .WithMany()
        //   .HasForeignKey("idProductStatus")
        //   .IsRequired()


        builder.HasOne(p => p.Category)
          .WithMany()
          .HasForeignKey("idCategory")
          .IsRequired();
    }
}
public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories_tb");

        builder.HasKey(c => c.Id).HasName("PK_categories_tb_idCategory");

        builder.HasIndex(c => c.Name).IsUnique().HasDatabaseName("UI_categories_tb_name");

        builder.Property(c => c.Id).HasColumnName("idCategory").IsRequired().HasConversion(id => id.Value, value => new IdCategory(value));
        builder.Property(c => c.Name).HasColumnName("name").IsRequired().HasMaxLength(256);
        builder.Property(c => c.Description).HasColumnName("description").HasMaxLength(1024);
        builder.Property(c => c.IsEnabled).HasColumnName("isEnabled").IsRequired();
        builder.Property(c => c.IdUserCreate).HasColumnName("idUserCreate").IsRequired().HasConversion(id => id.Value, value => new IdUser(value));
        builder.Property(c => c.DateCreate).HasColumnName("dateCreate").IsRequired();
        builder.Property(c => c.IdUserUpdate).HasColumnName("idUserUpdate").IsRequired().HasConversion(id => id.Value, value => new IdUser(value));
        builder.Property(c => c.DateUpdate).HasColumnName("dateUpdate").IsRequired();
        builder.Property(c => c.IdCategoryParent).HasColumnName("idCategoryParent").HasConversion(id => id != null ? id.Value : (Guid?)null,
                                                                         value => value.HasValue ? new IdCategory(value.Value) : null);
        builder.HasOne(c => c.CategoryParent)
               .WithMany(c => c.SubCategories)
               .HasForeignKey(sc => sc.IdCategoryParent)
               .HasConstraintName("FK_categories_tb_TO_categories_tb_idCategoryParent")
               .OnDelete(DeleteBehavior.Restrict);
    }
}