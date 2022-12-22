using AutoMapper;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Entities.Identity;
using FamilyFinance.Domain.Entities.Report;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Models.Identity;

namespace FamilyFinance.WebApi
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<BudgetItem, BudgetItemModel>().ReverseMap();
            CreateMap<FinancialOperation, FinancialOperationModel>().ReverseMap();
            CreateMap<UserOperation, UserOperationModel>().ReverseMap();

            CreateMap<DailyReport, ReportModel>().ReverseMap();
            CreateMap<PeriodReport, ReportModel>().ReverseMap();

            CreateMap<User, UserModel>().ReverseMap();
        }
    }
}
