namespace Web.API.Controllers.Accounts;

using AutoMapper;
using Services.Accounts.BankAccounts.Create;
using Services.Accounts.CardAccounts.Create;
using Services.Accounts.CardAccounts.Update;
using Services.Accounts.CashAccounts.Create;
using Services.Accounts.Update;
using Web.API.Models.Accounts;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CashAccountsCreateOrUpdateModel, CashAccountsUpdateMessage>();
        CreateMap<CashAccountsCreateOrUpdateModel, CashAccountsCreateRequest>();

        CreateMap<CardAccountsCreateOrUpdateModel, CardAccountsUpdateMessage>();
        CreateMap<CardAccountsCreateOrUpdateModel, CardAccountsCreateRequest>();

        CreateMap<BankAccountsCreateOrUpdateModel, BankAccountsUpdateMessage>();
        CreateMap<BankAccountsCreateOrUpdateModel, BankAccountsCreateRequest>();
    }
}