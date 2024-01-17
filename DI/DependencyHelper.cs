namespace DI;

using Common;
using DAL.Abstract;
using DAL.Abstract.Accounts;
using DAL.Abstract.Accounts.BankAccounts;
using DAL.Abstract.Accounts.CashAccounts;
using DAL.Abstract.Banks;
using DAL.Abstract.Core;
using DAL.Abstract.Currencies;
using DAL.Abstract.FamilyMembers;
using DAL.Abstract.Files;
using DAL.Abstract.Transaction;
using DAL.Abstract.TransactionCategories;
using DAL.Abstract.Users;
using DAL.EF;
using DAL.EF.Core;
using DAL.EF.Repositories;
using DAL.EF.Repositories.Banks;
using DAL.EF.Repositories.Currencies;
using DAL.EF.Repositories.FamilyMembers;
using DAL.EF.Repositories.Files;
using DAL.EF.Repositories.TransactionCategories;
using DAL.EF.Repositories.Transactions;
using DAL.EF.Repositories.Users;
using DAL.EF.Repositories.Users.Accounts;
using DAL.EF.Repositories.Users.Accounts.BankAccounts;
using DAL.EF.Repositories.Users.Accounts.CardAccounts;
using DAL.EF.Repositories.Users.Accounts.CashAccounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;
using Services.RabbitMq;
using Services.RabbitMq.Core;
using Services.Users;

public static class DependencyHelper
{
    public static void RegisterServices(
        IServiceCollection services, IConfiguration configuration,
        string environmentName)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.EnableDetailedErrors();
            options.EnableSensitiveDataLogging();

            string defaultConnectionString =
                new SqlConnectionStringBuilder(configuration, "Default").ToString();

            options.UseMySql(defaultConnectionString,
                ServerVersion.AutoDetect(defaultConnectionString)
            );
        });

        RegisterTemplateSettings(services, configuration);

        services.AddSingleton(configuration.GetSettings<RabbitMqSettings>(RabbitMqSettings.SectionName));
        services.AddTransient<IRabbitMqService, RabbitMqService>();

        if (environmentName != "IntegrationTests")
        {
            services.AddHostedService<ActivationUserEmailSendingResultMessageConsumer>();
            services.AddHostedService<SendUserForgotPasswordMailSendingResultMessageConsumer>();
        }

        services.AddTransient<IUnitOfWork, ApplicationUnitOfWork>();
        services.AddTransient<IUsersRepository, UsersRepository>();
        services.AddTransient<ICurrenciesRepository, CurrenciesRepository>();
        services.AddTransient<IAccountsRepository, AccountsRepository>();
        services.AddTransient<ICashAccountsRepository, CashAccountsRepository>();
        services.AddTransient<ICardAccountsRepository, CardAccountsRepository>();
        services.AddTransient<IBankAccountsRepository, BankAccountsRepository>();
        services.AddTransient<ITransactionsRepository, TransactionsRepository>();
        services.AddTransient<ITransactionCategoriesRepository, TransactionCategoriesRepository>();
        services.AddTransient<ITransactionCorrelationsRepository, TransactionCorrelationsRepository>();
        services.AddTransient<IOutgoingEmailTemplatesRepository, OutgoingEmailTemplatesRepository>();
        services.AddTransient<IOutgoingEmailsRepository, OutgoingEmailsRepository>();
        services.AddTransient<IBanksRepository, BanksRepository>();
        services.AddTransient<IFamilyMembersRepository, FamilyMembersRepository>();
        services.AddTransient<ILanguagesRepository, LanguagesRepository>();
        services.AddTransient<IFilesRepository, FilesRepository>();
        services.AddTransient<IGeoLocationPositionsRepository, GeoLocationPositionsRepository>();

        services.AddSingleton<IClock, Clock>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton(_ => configuration);

        services.AddTransient<IJwtUtils, JwtUtils>();

        services.AddSingleton<IClock>(new Clock());
        services.AddHttpClient();
    }

    private static void RegisterTemplateSettings(IServiceCollection services, IConfiguration configuration)
    {
        var templateSettings = new TemplateSettings
        {
            SiteName = configuration.GetValue<string>("SiteName")!,
            SiteUrl = configuration.GetValue<string>("SiteUrl")!
        };

        services.AddSingleton(templateSettings);
    }

    public static void Replace<TService, TImplementation>(this IServiceCollection serviceCollection)
        where TService : class
        where TImplementation : class, TService
    {
        ServiceDescriptor existedDescriptor = serviceCollection.Single(d => d.ServiceType == typeof(TService));

        if (serviceCollection.Remove(existedDescriptor))
        {
            serviceCollection.Add(new ServiceDescriptor(typeof(TService), typeof(TImplementation),
                existedDescriptor.Lifetime));
        }
        else
        {
            throw new ReplaceServiceException($"Could not remove the {typeof(TService)}");
        }
    }
}