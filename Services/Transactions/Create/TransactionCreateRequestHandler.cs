namespace Services.Transactions.Create;

using AutoMapper;
using DAL.Abstract.Accounts;
using DAL.Abstract.Core;
using Serilog;
using Services.Core;
using Services.Transactions.Create.IncomeOutcome;
using Services.Transactions.Create.Transfer;
using SlimMessageBus;

public class TransactionCreateRequestHandler(
    ILogger logger,
    IMessageBus messageBus,
    IUnitOfWork unitOfWork,
    IAccountsRepository accountsRepository,
    IMapper mapper)
    : RequestHandler<TransactionsCreateRequest, int>(logger)
{
    protected override async Task<int> DoOnHandle(TransactionsCreateRequest request)
    {
        await ValidateAccounts(request);

        return await new Func<Task<int>>(async () =>
        {
            if (request.DestinationAccountId == null)
            {
                return await HandleAsIncomeOutcomeTransaction(request);
            }

            return await HandleTransferTransaction(request);
        }).ExecuteInTransactionAsync(unitOfWork, request);
    }

    private async Task<int> HandleTransferTransaction(TransactionsCreateRequest request)
    {
        var transferTransactionCreateRequest = new TransferTransactionCreateRequest
        {
            ActorId = request.ActorId,
            Amount = request.Amount,
            CategoryId = request.CategoryId,
            Description = request.Description,
            DestinationAccountId = request.DestinationAccountId!.Value,
            ExchangeRate = request.ExchangeRate,
            SourceAccountId = request.SourceAccountId,
            TransactionDateTime = request.TransactionDateTime,
            DbContextTransaction = request.DbContextTransaction,
            FamilyMemberId = request.FamilyMemberId,
            Confirmed = request.Confirmed
        };

        return await messageBus.Send(transferTransactionCreateRequest);
    }

    private async Task<int> HandleAsIncomeOutcomeTransaction(TransactionsCreateRequest request)
    {
        var incomeOutcomeTransactionCreateRequest = mapper.Map<TransactionsCreateIncomeOutcomeRequest>(request);
        incomeOutcomeTransactionCreateRequest.TargetAccountId = request.SourceAccountId;

        return await messageBus.Send(incomeOutcomeTransactionCreateRequest);
    }

    private async Task ValidateAccounts(TransactionsCreateRequest request)
    {
        var accountIds = new HashSet<int>
        {
            request.SourceAccountId
        };

        if (request.DestinationAccountId != null)
        {
            accountIds.Add(request.DestinationAccountId.Value);
        }

        var accounts = await accountsRepository.GetByIds(accountIds.ToArray());

        var closedAccounts = accounts.Where(a => a.Closed).ToArray();

        if (closedAccounts.Any())
        {
            throw new ThereAreClosedAccountsException(closedAccounts);
        }
    }
}