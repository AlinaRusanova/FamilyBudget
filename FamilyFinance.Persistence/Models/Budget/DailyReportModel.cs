using FamilyFinance.Domain.Entities.Budget;

namespace FamilyFinance.Persistence.Models.Budget
{
    public class DailyReportModel
    {
        public DateOnly Date { get; set; }
        public int Incomes { get; set; }
        public int Expenses { get; set; }
        public IEnumerable<FinancialOperation> FinancialOperations { get; set; }
    }
}
