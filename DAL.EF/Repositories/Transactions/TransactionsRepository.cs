namespace DAL.EF.Repositories.Transactions;

using DAL.Abstract.Accounts;
using DAL.Abstract.Currencies;
using DAL.Abstract.FamilyMembers;
using DAL.Abstract.Transaction;
using DAL.EF.Core;
using DevExtreme.AspNet.Data;
using Domain;
using Domain.Finances.Transactions;
using Microsoft.EntityFrameworkCore;

public class TransactionsRepository(ApplicationDbContext applicationDbContext)
    : BaseRepository<Transaction>(applicationDbContext), ITransactionsRepository
{
    private IQueryable<DayTranslation> DayTranslations =>
        EfRepository.DbContext.DayTranslations;

    private IQueryable<MonthTranslation> MonthTranslations =>
        EfRepository.DbContext.MothTranslations;

    private IQueryable<TransactionCorrelation> TransactionCorrelations =>
        EfRepository.DbContext.TransactionCorrelations;

    private IQueryable<Transaction> Transactions => EfRepository.Get();

    public async Task<bool> AreAnyTransactionWithAccounts(int[] accountIds)
    {
        return await EfRepository.Get(e => accountIds.Contains(e.AccountId)).AnyAsync();
    }

    public async Task<int> Add(Transaction transaction)
    {
        await EfRepository.AddAsync(transaction);

        return transaction.Id;
    }

    public async Task Update(Transaction transaction)
    {
        await EfRepository.UpdateAsync(transaction);
    }

    public async Task<Transaction?> GetById(int id)
    {
        return await EfRepository.GetByIdAsync(id);
    }

    public async Task Delete(int id)
    {
        await EfRepository.DeleteAsync(id);
    }

    public async Task<int[]> GetAccountIdsByTransactionIds(int[] transactionIds)
    {
        var query = from t in Transactions
            where transactionIds.Contains(t.Id)
            select t.AccountId;

        return await query.Distinct().ToArrayAsync();
    }

    public async Task<object> Get(TransactionsDataSourceLoadOptions loadOptions, int actorId, int languageId)
    {
        var query = from t in Transactions
                .Include(t => t.Account).ThenInclude(a => a.Currency)
                .Include(t => t.Category)
                .Include(t => t.FamilyMember)
            join tc in TransactionCorrelations
                on t.TransactionCorrelationId equals tc.Id into jTransactionCorrelations
            from
                joinedTransactionCorrelation in jTransactionCorrelations.DefaultIfEmpty()
            join incomeTransaction in Transactions.Include(t => t.Account)
                on joinedTransactionCorrelation.IncomeTransactionId equals incomeTransaction.Id into
                joinedIncomeTransactions
            from joinedIncomeTransaction in joinedIncomeTransactions.DefaultIfEmpty()
            join mt in MonthTranslations.Include(e => e.Language)
                on new
                {
                    MonthNumber = t.MonthOfYear,
                    LanguageId = languageId
                }
                equals new
                {
                    mt.MonthNumber,
                    mt.LanguageId
                } into jMonthTranslations
            from joinedMonthTranslation in jMonthTranslations.DefaultIfEmpty()
            join dt in DayTranslations.Include(e => e.Language)
                on new
                {
                    DayNumber = t.DayOfWeek,
                    LanguageId = languageId
                }
                equals new
                {
                    dt.DayNumber,
                    dt.LanguageId
                } into jDayTranslations
            from joinedDayTranslation in jDayTranslations.DefaultIfEmpty()
            where loadOptions.AccountId != null || t.ActorId == actorId
            select new TransactionItem
            {
                Id = t.Id,
                Confirmed = t.Confirmed,
                Amount = t.Amount,
                CategoryName = t!.Category != null ? t!.Category.Name : "",
                Description = t.Description,
                TransactionDateTime = t.TransactionDateTime,
                TransactionWeekOfYear = t.TransactionWeekOfYear,
                Month = joinedMonthTranslation.Value,
                Year = t.TransactionDateTime.Year,
                DayOfMonth = t.TransactionDateTime.Day,
                DayOfWeek = joinedDayTranslation.Value,
                Account = new AccountDto
                {
                    Id = t.Account.Id,
                    Name = t.Account.Name,
                    Closed = t.Account.Closed,
                    Currency = new CurrencyDto
                    {
                        Id = t.Account.Currency.Id,
                        Name = t.Account.Currency.Name,
                        IsoCode = t.Account.Currency.IsoCode,
                        SymbolOrAbbrev = t.Account.Currency.SymbolOrAbbrev
                    }
                },
                Type = t.Amount > 0 ? TransactionType.Debit : TransactionType.Credit,
                DebitAccount = joinedIncomeTransaction != null
                    ? new AccountDto
                    {
                        Id = joinedIncomeTransaction.AccountId,
                        Name = joinedIncomeTransaction.Account.Name,
                        Closed = joinedIncomeTransaction.Account.Closed
                    }
                    : null,
                FamilyMember = t.FamilyMember != null
                    ? new FamilyMemberDto
                    {
                        Id = t.FamilyMember.Id,
                        Name = t.FamilyMember.Name,
                        OwnerId = t.FamilyMember.OwnerId
                    }
                    : null,
                Locked = t.Account.Closed || (joinedIncomeTransaction != null && joinedIncomeTransaction.Account.Closed)
            };

        return await Task.FromResult(DataSourceLoader.Load(query, loadOptions));
    }

    public async Task<int[]> GetCategoryIds(int actorId)
    {
        return await EfRepository.Get(e => e.ActorId == actorId)
            .Select(e => e.CategoryId ?? 0)
            .Distinct()
            .ToArrayAsync();
    }

    public async Task<bool> ContainsLocked(int[] transactionIds)
    {
        var query = from t in Transactions
                .Include(t => t.Account).ThenInclude(a => a.Currency)
                .Include(t => t.Category)
                .Include(t => t.FamilyMember)
            join tc in TransactionCorrelations
                on t.TransactionCorrelationId equals tc.Id into jTransactionCorrelations
            from
                joinedTransactionCorrelation in jTransactionCorrelations.DefaultIfEmpty()
            join incomeTransaction in Transactions.Include(t => t.Account)
                on joinedTransactionCorrelation.IncomeTransactionId equals incomeTransaction.Id into
                joinedIncomeTransactions
            from joinedIncomeTransaction in joinedIncomeTransactions.DefaultIfEmpty()
            where transactionIds.Contains(t.Id)
            select t.Account.Closed || (joinedIncomeTransaction != null && joinedIncomeTransaction.Account.Closed);

        return await query.AnyAsync(b => b);
    }
}