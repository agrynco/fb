namespace Services.Transactions.Create.IncomeOutcome;

using AutoMapper;
using Domain.Finances.Transactions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TransactionsCreateIncomeOutcomeRequest, Transaction>();
    }
}