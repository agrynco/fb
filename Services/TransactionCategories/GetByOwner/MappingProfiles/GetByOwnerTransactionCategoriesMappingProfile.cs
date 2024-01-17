using AutoMapper;
using Domain;
using Domain.Finances.Transactions;

namespace Services.TransactionCategories.GetByOwner.MappingProfiles;

public class GetByOwnerTransactionCategoriesMappingProfile : Profile
{
    public GetByOwnerTransactionCategoriesMappingProfile()
    {
        CreateMap<TransactionCategory, GetByOwnerTransactionCategoryItem>();
        CreateMap<User, GetByOwnerTransactionCategoryOwner>();
    }
}