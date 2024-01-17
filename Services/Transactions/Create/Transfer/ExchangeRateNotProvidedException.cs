using Services.Exceptions;

namespace Services.Transactions.Create.Transfer;

public class ExchangeRateNotProvidedException : ServiceException
{
    public ExchangeRateNotProvidedException(TransferTransactionCreateRequest transferTransactionCreateRequest) : base(
        "Exchange rate not provided.")
    {
        TransferTransactionCreateRequest = transferTransactionCreateRequest;
    }

    public TransferTransactionCreateRequest TransferTransactionCreateRequest { get; }
}