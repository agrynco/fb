using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations;

public class TransactionCorrelationEntityConfiguration : IEntityTypeConfiguration<TransactionCorrelation>
{
    public void Configure(EntityTypeBuilder<TransactionCorrelation> builder)
    {
        builder.HasIndex(p => p.IncomeTransactionId).IsUnique();
        builder.HasIndex(p => p.OutcomeTransactionId).IsUnique();
    }
}