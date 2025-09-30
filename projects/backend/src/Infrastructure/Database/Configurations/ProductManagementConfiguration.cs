using Domain.ProductManagement;
using Domain.ProductManagement.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shared.ValueObjects;

namespace Infrastructure.Database.Configurations;

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
        builder.OwnsOne(p => p.Dimension, dimensionBuilder =>
        {
            dimensionBuilder.Property(d => d.Height).HasColumnName("height").IsRequired();
            dimensionBuilder.Property(d => d.Width).HasColumnName("width").IsRequired();
            dimensionBuilder.Property(d => d.Depth).HasColumnName("depth").IsRequired();
            dimensionBuilder.Property(d => d.IsBulky).HasColumnName("isBulky").IsRequired();
        });
        builder.Property(p => p.DatabaseVersion).HasColumnName("databaseVersion").IsRequired();
        builder.Property(p => p.IdUserCreate).HasColumnName("idUserCreate").IsRequired()
                                             .HasConversion(id => id.Value.ToString(), value => new IdUser(new Guid(value)));
        builder.Property(p => p.DateCreate).HasColumnName("dateCreate").IsRequired();
        builder.Property(p => p.IdUserUpdate).HasColumnName("idUserUpdate")
                                             .HasConversion(id => id != null ? id.Value.ToString() : (string?)null, value => value != null ? new IdUser(new Guid(value)) : null);
        builder.Property(p => p.DateUpdate).HasColumnName("dateUpdate");
        builder.Property(p => p.DatabaseVersion).HasColumnName("databaseVersion").IsRequired();
    }
}