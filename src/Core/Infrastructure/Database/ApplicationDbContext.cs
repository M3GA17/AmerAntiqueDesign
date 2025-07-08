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
            //&& entry.State == EntityState.Modified
            if (entry.Entity is IAggregateRoot aggregateRoot && (entry.State != EntityState.Added || entry.State != EntityState.Deleted))
            {
                aggregateRoot.IncrementVersion();
            }
        }
        // Usiamo un HashSet per evitare di incrementare la versione dello stesso Aggregate Root più volte
        var modifiedAggregateRoots = new HashSet<IAggregateRoot>();

        //foreach (var entry in ChangeTracker.Entries())
        //{
        //    // Se l'entità è stata aggiunta, modificata o eliminata
        //    if (entry.State == EntityState.Added ||
        //        entry.State == EntityState.Modified)
        //    {
        //        // Se l'entità è direttamente un Aggregate Root modificato/aggiunto/eliminato
        //        if (entry.Entity is IAggregateRoot aggregateRoot)
        //        {
        //            modifiedAggregateRoots.Add(aggregateRoot);
        //        }
        //        else // Se è un'entità o Value Object interno, dobbiamo trovare il suo Aggregate Root
        //        {

        //            // Un'implementazione robusta richiede di capire come le tue Entity figlie sono collegate
        //            // all'Aggregate Root.
        //            // Se la Entity figlia ha una proprietà di navigazione verso l'Aggregate Root padre:
        //            foreach (var navigationEntry in entry.Navigations)
        //            {
        //                if (navigationEntry.IsLoaded && navigationEntry.CurrentValue is IAggregateRoot parentAggregate)
        //                {
        //                    // Questo assume che la navigazione sia già stata caricata e che sia l'Aggregate Root.
        //                    // Potrebbe non essere sempre vero, specialmente con lazy loading disabilitato.
        //                    modifiedAggregateRoots.Add(parentAggregate);
        //                }
        //                //else if (navigationEntry.Metadata.IsOnDependent && navigationEntry.Metadata.TargetEntityType.ClrType.IsAssignableTo(typeof(IAggregateRoot)))
        //                //{
        //                //    // Se la navigazione è una FK che punta all'Aggregate Root
        //                //    var fkValue = navigationEntry.CurrentValue; // Potrebbe essere null se non ancora assegnata

        //                //    // Per un approccio più robusto, dovremmo recuperare l'Aggregate Root
        //                //    // tramite il DbContext se non è già caricato.
        //                //    // Questo potrebbe generare query extra se l'AR non è tracciato.
        //                //    var relatedEntry = ChangeTracker.Entries().FirstOrDefault(e => e.Entity is IAggregateRoot ar && ar..Equals(fkValue)); // Questo è un esempio molto semplificato, Id è un ValueObject!
        //                //    if (relatedEntry?.Entity is IAggregateRoot trackedAggregate)
        //                //    {
        //                //        modifiedAggregateRoots.Add(trackedAggregate);
        //                //    }
        //                //}
        //            }
        //        }
        //    }
        //}


        //foreach (var aggregateRoot in modifiedAggregateRoots)
        //{
        //    aggregateRoot.IncrementVersion();
        //    // Marca l'Aggregate Root come modificato nel Change Tracker se non lo è già,
        //    // per assicurare che la versione venga salvata.
        //    Entry(aggregateRoot).State = EntityState.Modified;
        //}
    }
}


