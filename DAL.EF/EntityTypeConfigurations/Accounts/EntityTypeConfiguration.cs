namespace DAL.EF.EntityTypeConfigurations.Accounts;

using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
    where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(e => e.Created).HasDefaultValueSql("(UTC_TIMESTAMP)");
        builder.Property(e => e.Updated).HasDefaultValueSql("(UTC_TIMESTAMP)");
    }
}