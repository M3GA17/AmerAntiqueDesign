namespace Infrastructure.Persistence.Configurations;


//public class AspNetUserConfiguration : IEntityTypeConfiguration<DomainUser>
//{
//    public void Configure(EntityTypeBuilder<DomainUser> builder)
//    {
//        builder.ToTable("AspNetUsers", "identity");

//        builder.HasIndex(e => e.NormalizedEmail, "EmailIndex");

//        builder.HasIndex(e => e.NormalizedUserName, "UserNameIndex").IsUnique();

//        builder.Property(e => e.Id).ValueGeneratedNever();
//        builder.Property(e => e.Email).HasMaxLength(256);
//        builder.Property(e => e.NormalizedEmail).HasMaxLength(256);
//        builder.Property(e => e.NormalizedUserName).HasMaxLength(256);
//        builder.Property(e => e.UserName).HasMaxLength(256);

//        //builder.HasMany(d => d.Roles).WithMany(p => p.Users)
//        //    .UsingEntity<Dictionary<string, object>>(
//        //        "AspNetUserRole",
//        //        r => r.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
//        //        l => l.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
//        //        j =>
//        //        {
//        //            j.HasKey("UserId", "RoleId");
//        //            j.ToTable("AspNetUserRoles", "identity");
//        //            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
//        //        });
//    }
//}
//public class AspNetRoleConfiguration : IEntityTypeConfiguration<AspNetRole>
//{
//    public void Configure(EntityTypeBuilder<AspNetRole> builder)
//    {
//        builder.ToTable("AspNetRoles", "identity");

//        builder.HasIndex(e => e.NormalizedName, "RoleNameIndex").IsUnique();

//        builder.Property(e => e.Id).ValueGeneratedNever();
//        builder.Property(e => e.Name).HasMaxLength(256);
//        builder.Property(e => e.NormalizedName).HasMaxLength(256);
//    }
//}
//public class AspNetUserClaimConfiguration : IEntityTypeConfiguration<AspNetUserClaim>
//{
//    public void Configure(EntityTypeBuilder<AspNetUserClaim> builder)
//    {
//        builder.ToTable("AspNetUserClaims", "identity");

//        builder.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

//        builder.HasOne(d => d.User).WithMany(p => p.AspNetUserClaims).HasForeignKey(d => d.UserId);
//    }
//}
////public class AspNetUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<Guid>>
////{
////    public void Configure(EntityTypeBuilder<IdentityUserRole<Guid>> builder)
////    {
////        builder.ToTable("AspNetUserRoles", "identity");
////    }
////}
//public class AspNetUserLoginConfiguration : IEntityTypeConfiguration<AspNetUserLogin>
//{
//    public void Configure(EntityTypeBuilder<AspNetUserLogin> builder)
//    {
//        builder.HasKey(e => new { e.LoginProvider, e.ProviderKey });

//        builder.ToTable("AspNetUserLogins", "identity");

//        builder.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

//        builder.Property(e => e.LoginProvider).HasMaxLength(128);
//        builder.Property(e => e.ProviderKey).HasMaxLength(128);

//        builder.HasOne(d => d.User).WithMany(p => p.AspNetUserLogins).HasForeignKey(d => d.UserId);
//    }
//}
//public class AspNetRoleClaimConfiguration : IEntityTypeConfiguration<AspNetRoleClaim>
//{
//    public void Configure(EntityTypeBuilder<AspNetRoleClaim> builder)
//    {
//        builder.ToTable("AspNetRoleClaims", "identity");

//        builder.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

//        builder.HasOne(d => d.Role).WithMany(p => p.AspNetRoleClaims).HasForeignKey(d => d.RoleId);
//    }
//}
//public class AspNetUserTokenConfiguration : IEntityTypeConfiguration<AspNetUserToken>
//{
//    public void Configure(EntityTypeBuilder<AspNetUserToken> builder)
//    {
//        builder.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

//        builder.ToTable("AspNetUserTokens", "identity");

//        builder.Property(e => e.LoginProvider).HasMaxLength(128);
//        builder.Property(e => e.Name).HasMaxLength(128);

//        builder.HasOne(d => d.User).WithMany(p => p.AspNetUserTokens).HasForeignKey(d => d.UserId);
//    }
//}