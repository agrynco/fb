namespace Services.TransactionCategories.GetById;

using AutoMapper;
using Domain.Finances.Transactions;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<TransactionCategory, TransactionCategoriesGetByIdResponse>();
    }
}