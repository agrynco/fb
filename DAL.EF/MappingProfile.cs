namespace DAL.EF;

using AutoMapper;
using DAL.Abstract.Accounts.CashAccounts;
using Domain.Finances;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Bank, BankDto>();
    }
}