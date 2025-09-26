using Domain.UserManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Database;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // 1. Mappatura della tabella
        builder.ToTable("user_entity", "keycloak");

        // 2. Chiave primaria e Indici
        builder.HasKey(u => u.Id).HasName("constraint_fb");
        builder.HasIndex(u => u.Username).IsUnique().HasDatabaseName("idx_user_service_account");
        builder.HasIndex(u => u.Email).IsUnique().HasDatabaseName("idx_user_email");

        // 3. Configurazione delle proprietà
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasConversion(id => id.Value.ToString(), value => new IdUser(new Guid(value)));

        builder.Property(u => u.Username)
            .HasColumnName("username")
            .HasMaxLength(256);

        builder.Property(u => u.Email)
            .HasColumnName("email")
            .HasMaxLength(256);

        builder.Property(u => u.FirstName)
            .HasColumnName("first_name")
            .HasMaxLength(256);

        builder.Property(u => u.LastName)
            .HasColumnName("last_name")
            .HasMaxLength(256);

        builder.Property(u => u.IsEnabled)
            .HasColumnName("enabled")
            .IsRequired();

        builder.Property(u => u.DateCreate)
            .HasColumnName("created_timestamp")
            .HasConversion
            (
                dto => dto.ToUnixTimeMilliseconds(),
                value => DateTimeOffset.FromUnixTimeMilliseconds(value)
            )
            .IsRequired();

        builder.Ignore(u => u.DatabaseVersion);
        builder.Ignore(u => u.DateUpdate);
    }
}
public class KeycloakRoleConfiguration : IEntityTypeConfiguration<KeycloakRole>
{
    public void Configure(EntityTypeBuilder<KeycloakRole> builder)
    {
        // 1. Mappatura della tabella
        builder.ToTable("keycloak_role", "keycloak");

        // 2. Chiave primaria e Indici
        builder.HasKey(kr => kr.Id);
        builder.HasIndex(kr => kr.Name).HasDatabaseName("idx_keycloak_role_name");

        // 3. Configurazione delle proprietà
        builder.Property(u => u.Id)
            .HasColumnName("id")
            .IsRequired()
            .HasConversion(id => id.Value.ToString(), value => new IdKeycloakRole(new Guid(value)));

        builder.Property(kr => kr.Name)
            .HasColumnName("name")
            .HasMaxLength(255);

        builder.Property(kr => kr.Description)
            .HasColumnName("description")
            .HasMaxLength(255);

        builder.Ignore(u => u.DateCreate);
        builder.Ignore(u => u.DateUpdate);
    }
}
public class UserRoleMappingConfiguration : IEntityTypeConfiguration<UserRoleMapping>
{
    public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
    {
        builder.ToTable("user_role_mapping", "keycloak");
        builder.HasKey(urm => new { urm.Id1, urm.Id2 }).HasName("constraint_c");


        builder.Property(urm => urm.Id1).HasColumnName("user_id").IsRequired()
          .HasConversion(id => id.Value.ToString(), value => new IdUser(new Guid(value)));

        builder.Property(u => u.Id2).HasColumnName("role_id").IsRequired()
         .HasConversion(id => id.Value.ToString(), value => new IdKeycloakRole(new Guid(value)));

        builder.HasOne(c => c.User)
       .WithMany(u => u.UserRoleMappings)
       .HasForeignKey(u => u.Id1)
       .HasConstraintName("fk_c4fqv34p1mbylloxang7b1q3l")
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Role)
           .WithMany(urm => urm.UserRoleMappings)
           .HasForeignKey(urm => urm.Id2)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(u => u.DateCreate);
        builder.Ignore(u => u.DateUpdate);
    }
}
public class GroupRoleMappingConfiguration : IEntityTypeConfiguration<GroupRoleMapping>
{
    public void Configure(EntityTypeBuilder<GroupRoleMapping> builder)
    {
        // 1. Mappatura della tabella
        builder.ToTable("group_role_mapping", "keycloak");

        builder.HasKey(urm => new { urm.Id1, urm.Id2 }).HasName("constraint_group_role");
        builder.HasIndex(kr => kr.Id1).HasDatabaseName("idx_group_role_mapp_group");


        builder.Property(urm => urm.Id1)
          .HasColumnName("group_id")
          .IsRequired()
          .HasConversion(id => id.Value.ToString(), value => new IdKeycloakGroup(new Guid(value)));

        builder.Property(u => u.Id2)
         .HasColumnName("role_id")
         .IsRequired()
         .HasConversion(id => id.Value.ToString(), value => new IdKeycloakRole(new Guid(value)));

        builder.HasOne(c => c.Group)
       .WithMany(kg => kg.GroupRoleMappings)
       .HasForeignKey(u => u.Id1)
       .HasConstraintName("fk_group_role_group")
       .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(c => c.Role)
           .WithMany(kr => kr.GroupRoleMappings)
           .HasForeignKey(u => u.Id2)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Ignore(u => u.DateCreate);
        builder.Ignore(u => u.DateUpdate);
    }
}
public class UserGroupMembershipConfiguration : IEntityTypeConfiguration<UserGroupMembership>
{
    public void Configure(EntityTypeBuilder<UserGroupMembership> builder)
    {
        builder.ToTable("user_group_membership", "keycloak");
        builder.HasKey(urm => new { urm.Id1, urm.Id2 }).HasName("constraint_user_group");

        // Id1 = user_id
        builder.Property(urm => urm.Id1).HasColumnName("user_id").IsRequired()
               .HasConversion(id => id.Value.ToString(), value => new IdUser(new Guid(value)));
        // Id2 = group_id
        builder.Property(u => u.Id2).HasColumnName("group_id").IsRequired()
                .HasConversion(id => id.Value.ToString(), value => new IdKeycloakGroup(new Guid(value)));

        // Relazione con User: la FK è Id1
        builder.HasOne(usm => usm.User)
               .WithMany(u => u.UserGroupMemberships)
               .HasForeignKey(usm => usm.Id1) // Usa la variabile 'usm'
               .HasConstraintName("fk_user_group_user");

        // Relazione con Group: la FK è Id2
        builder.HasOne(usm => usm.Group)
               .WithMany(g => g.UserGroupMemberships)
               .HasForeignKey(usm => usm.Id2);

        builder.Ignore(u => u.DateCreate);
        builder.Ignore(u => u.DateUpdate);
    }
}
public class KeycloakGroupConfiguration : IEntityTypeConfiguration<KeycloakGroup>
{

    public void Configure(EntityTypeBuilder<KeycloakGroup> builder)
    {
        // 1. Mappatura della tabella
        builder.ToTable("keycloak_group", "keycloak");

        // 2. Chiave primaria e Indici
        builder.HasKey(kg => kg.Id);

        // 3. Configurazione delle proprietà
        builder.Property(u => u.Id)
               .HasColumnName("id")
               .IsRequired()
               .HasConversion(id => id.Value.ToString(), value => new IdKeycloakGroup(new Guid(value)));

        builder.Property(kg => kg.Name)
            .HasColumnName("name")
            .HasMaxLength(255);

        builder.Property(kr => kr.Description)
        .HasColumnName("description")
        .HasMaxLength(255);

        builder.Ignore(u => u.DateCreate);
        builder.Ignore(u => u.DateUpdate);
    }
}