namespace Services.Transactions.Update;

using AutoMapper;
using Services.Transactions.Create;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TransactionUpdateMessage, TransactionsCreateRequest>();
    }
}