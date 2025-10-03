using Application.Abstractions.UnitOfWork;
using Domain.ProductManagement;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Shared.Primitives.Interfaces;
using Shared.ValueObjects;

namespace Infrastructure.Database;

public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator)
                   : DbContext(options), IApplicationDbContext, IUnitOfWork
{
    //dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=AmerAntiqueDesign_Dev;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Persistence/Entities --context-dir Persistence/Context --context TuoDbContext --no-onconfiguring

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseNpgsql();
    //}

    private readonly IMediator mediator = mediator;
    public virtual DbSet<Product> Products { get; set; }

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
        //IncrementDatabaseVersion();
        var entries = ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            // Salva informazioni sull'entry, l'utente e le modifiche
            // in una tabella di Audit
        }

        await DispatchDomainEventsAsync();
        return await base.SaveChangesAsync(cancellationToken);
    }
    private async Task DispatchDomainEventsAsync()
    {
        var domainEventEntities = ChangeTracker.Entries<IAggregateRoot<BaseId<Guid>>>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .ToArray();

        foreach (var entity in domainEventEntities)
        {
            var events = entity.DomainEvents.ToArray();
            entity.ClearDomainEvents();
            foreach (var domainEvent in events)
            {
                await mediator.Publish(domainEvent);
            }
        }
    }
    //private void IncrementDatabaseVersion()
    //{
    //    //foreach (var entry in ChangeTracker.Entries())
    //    //{
    //    //    //&& entry.State == EntityState.Modified
    //    //    if (entry.Entity is IAggregateRoot aggregateRoot && (entry.State != EntityState.Added || entry.State != EntityState.Deleted))
    //    //    {
    //    //        aggregateRoot.IncrementVersion();
    //    //    }
    //    //}
    //}
}


