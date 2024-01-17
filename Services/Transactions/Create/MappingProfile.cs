namespace Services.Transactions.Create;

using AutoMapper;
using Services.Transactions.Create.IncomeOutcome;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TransactionsCreateRequest, TransactionsCreateIncomeOutcomeRequest>();
    }
}