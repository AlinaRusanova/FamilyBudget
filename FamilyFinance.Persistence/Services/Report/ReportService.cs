using AutoMapper;
using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Entities.Report;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using FamilyFinance.Exceptions.Exceptions;
using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.Persistence.Services.IServices.Report;

namespace FamilyFinance.Persistence.Services.Report
{
    public class ReportService : IReportService<ReportModel>
    {
        private readonly IUserOperationRepository _userOperationRepository;
        private readonly IMapper _mapper;

        public ReportService(IUserOperationRepository userOperationRepository, IMapper mapper)
        {
            if (userOperationRepository == null || mapper == null)
            {
                throw new BadRequestException(nameof(ReportService));
            }

            _userOperationRepository = userOperationRepository;
            _mapper = mapper;
        }


        public async Task<ReportModel> GetDailyReportAsync(DateTime date, int id, CancellationToken ct)
        {
            IEnumerable<UserOperation> entities = await _userOperationRepository.ListAllAsync(ct);

            var listOfOperations = entities.Where(x => x.Date == date && x.UserId == id).ToList();

            var report = new DailyReport
            {
                Date = date,
                Incomes = listOfOperations.Where(x => x.BudgetItem != null && x.BudgetItem.BudgetType == BudgetType.Income).Sum(x => x.SumBudgetItem),
                Expenses = listOfOperations.Where(x => x.BudgetItem != null && x.BudgetItem.BudgetType == BudgetType.Expense).Sum(x => x.SumBudgetItem),
                FinancialOperations = listOfOperations.Where(x => x.FinOperation != null && x.SumFinOperation != 0).Select(x => x.FinOperation).ToList(),
                UserOperations = listOfOperations.Where(x => x.BudgetItem != null || x.FinOperation != null)
            };

            report.Profit = report.Incomes - report.Expenses;

            return _mapper.Map<ReportModel>(report);
        }


        public async Task<ReportModel> GetPeriodReportAsync(DateTime dateFrom, DateTime dateTo, int id, CancellationToken ct)
        {
            if(dateFrom == null || dateTo == null)
            {
                throw new BadRequestException(nameof(ReportModel));
            }

            IEnumerable<UserOperation> entities = await _userOperationRepository.ListAllAsync(ct);

            var listOfOperations = entities.Where(x => x.Date >= dateFrom && x.Date <= dateTo && x.UserId == id).ToList();

            var report = new PeriodReport
            {
                DateFrom = dateFrom,
                DateTo = dateTo,
                Incomes = listOfOperations.Where(x => x.BudgetItem != null && x.BudgetItem.BudgetType == BudgetType.Income).Sum(x => x.SumBudgetItem),
                Expenses = listOfOperations.Where(x => x.BudgetItem != null && x.BudgetItem.BudgetType == BudgetType.Expense).Sum(x => x.SumBudgetItem),
                FinancialOperations = listOfOperations.Where(x => x.FinOperation != null && x.SumFinOperation != 0).Select(x => x.FinOperation).ToList(),
                UserOperations = listOfOperations.Where(x => x.BudgetItem != null || x.FinOperation != null)
            };

            report.Profit = report.Incomes - report.Expenses;

            return _mapper.Map<ReportModel>(report);
        }
    }
}
