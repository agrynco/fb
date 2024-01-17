using DAL.EF.EntityTypeConfigurations.Accounts;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.EF.EntityTypeConfigurations.TransactionCategories;

public class TransactionCategoryEntityConfiguration : EntityTypeConfiguration<TransactionCategory>
{
    public override void Configure(EntityTypeBuilder<TransactionCategory> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).IsRequired().HasMaxLength(250);

        builder.HasIndex(p => new
        {
            p.Name,
            p.ParentId,
            p.OwnerId
        });

        var items = new List<TransactionCategory>(DefaultTransactionCategoriesFactory.Categories);

        const int TES_USER_ID = 3;
        items.AddRange(DefaultTransactionCategoriesFactory.BuildCategories(TES_USER_ID));

        builder.HasData(items);
    }
}