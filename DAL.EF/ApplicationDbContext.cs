namespace DAL.EF;

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Common;
using Domain;
using Domain.Finances;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;

public class ApplicationDbContext : DbContext
{
    private readonly IClock _clock;

    public ApplicationDbContext(DbContextOptions options, IClock clock) : base(options) { _clock = clock; }

    public ApplicationDbContext(DbContextOptions options) : base(options) { _clock = new Clock(); }

    protected ApplicationDbContext(IClock clock) { _clock = clock; }

    public DbSet<Account> Accounts { get; set; } = null!;
    public DbSet<BankAccount> BankAccounts { get; set; } = null!;

    public DbSet<Bank> Banks { get; set; } = null!;
    public DbSet<CardAccount> CardAccounts { get; set; } = null!;
    public DbSet<CashAccount> CashAccounts { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Currency> Currencies { get; set; } = null!;
    public DbSet<DayTranslation> DayTranslations { get; set; } = null!;

    public DbSet<FamilyMember> FamilyMembers { get; set; } = null!;

    public DbSet<File> Files { get; set; } = null!;
    public DbSet<GeoLocationPosition> GeoLocationPositions { get; set; } = null!;

    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<MonthTranslation> MothTranslations { get; set; } = null!;

    public DbSet<OutgoingEmail> OutgoingEmails { get; set; } = null!;
    public DbSet<OutgoingEmailTemplate> OutgoingEmailTemplates { get; set; } = null!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;
    public DbSet<TransactionCategory> TransactionCategories { get; set; } = null!;
    public DbSet<TransactionCorrelation> TransactionCorrelations { get; set; } = null!;
    public DbSet<Transaction> Transactions { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetAssembly(typeof(ApplicationDbContext))!);

        base.OnModelCreating(modelBuilder);
    }

    [SuppressMessage("ReSharper", "SuspiciousTypeConversion.Global")]
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (EntityEntry changedEntity in ChangeTracker.Entries())
        {
            if (changedEntity.Entity is IEntity entity)
            {
                switch (changedEntity.State)
                {
                    case EntityState.Added:
                        entity.Created = _clock.UtcNow;
                        break;
                    case EntityState.Modified:
                        entity.Updated = _clock.UtcNow;
                        break;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}