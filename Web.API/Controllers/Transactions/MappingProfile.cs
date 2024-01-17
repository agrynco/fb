namespace Web.API.Controllers.Transactions;

using AutoMapper;
using Domain;
using Services.Transactions.Create;
using Services.Transactions.Update;
using Web.API.Models.Transactions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TransactionCreateOrUpdateModel, TransactionsCreateRequest>();
        CreateMap<GeoLocationPositionModel, GeoLocationPosition>();
        CreateMap<TransactionCreateOrUpdateModel, TransactionUpdateMessage>();
    }
}