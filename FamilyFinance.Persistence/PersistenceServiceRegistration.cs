using FamilyFinance.Domain;
using FamilyFinance.Domain.Repositories;
using FamilyFinance.Domain.Repositories.Budget;
using FamilyFinance.Domain.Repositories.Identity;
using FamilyFinance.Domain.Repositories.IRepositories;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Identity;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.Persistence.Services.Budget;
using FamilyFinance.Persistence.Services.Identity;
using FamilyFinance.Persistence.Services.IServices.Budget;
using FamilyFinance.Persistence.Services.IServices.Identity;
using FamilyFinance.Persistence.Services.IServices.Report;
using FamilyFinance.Persistence.Services.Report;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FamilyFinance.Persistence
{
    public static class PersistenceServiceRegistration
    {
        private const string ConnectionString = "FFDbConnectionStrings";

        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(connectionString))
                connectionString = ConnectionString;


            services.AddDbContext<FFDbContext>(opts =>
            {
                opts.UseSqlServer(configuration.GetConnectionString(connectionString), x => x.MigrationsAssembly("FamilyFinance.Persistence"));
            });

           

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IBudgetItemRepository, BudgetItemRepository>();
            services.AddScoped<IFinancialOperationRepository, FinancialOperationRepository>();
            services.AddScoped<IUserOperationRepository, UserOperationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IBudgetItemService<BudgetItemModel>, BudgetItemService>();
            services.AddScoped<IFinancialOperationService<FinancialOperationModel>, FinancialOperationService>();
            services.AddScoped<IUserOperationService<UserOperationModel>, UserOperationService>();
            services.AddScoped<IReportService<ReportModel>, ReportService>();
            services.AddScoped<IUserService<UserModel>, UserService>();

            return services;
        }

    }
}
