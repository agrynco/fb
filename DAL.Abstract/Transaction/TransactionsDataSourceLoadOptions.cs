using DevExtreme.AspNet.Mvc;

namespace DAL.Abstract.Transaction;

public class TransactionsDataSourceLoadOptions : DataSourceLoadOptions
{
    public int? AccountId { get; set; }
}