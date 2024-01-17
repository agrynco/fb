namespace Web.API.Models;

using DAL.Abstract.Transaction;
using DevExtreme.AspNet.Data.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

[ModelBinder(BinderType = typeof(TransactionsDataSourceLoadOptionsModelBinder))]
public class TransactionsDataSourceLoadOptionsModel : TransactionsDataSourceLoadOptions
{
}

public class TransactionsDataSourceLoadOptionsModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var sourceLoadOptions = new TransactionsDataSourceLoadOptionsModel();

        Func<string, string> valueSource = key =>
            bindingContext.ValueProvider.GetValue(key).FirstOrDefault();

        DataSourceLoadOptionsParser.Parse(sourceLoadOptions, valueSource);

        ParseAccountId(sourceLoadOptions, valueSource);

        bindingContext.Result = ModelBindingResult.Success(sourceLoadOptions);

        return Task.FromResult(true);
    }

    private void ParseAccountId(TransactionsDataSourceLoadOptionsModel loadOptions, Func<string, string> valueSource)
    {
        //ToDo: Rename transactionId to accountId
        string transactionId = valueSource("transactionId");
        if (!string.IsNullOrEmpty(transactionId))
        {
            loadOptions.AccountId = int.Parse(transactionId);
        }
    }
}