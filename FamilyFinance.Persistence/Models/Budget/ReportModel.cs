using FamilyFinance.Domain.Entities.Budget;

namespace FamilyFinance.Persistence.Models.Budget
{
    public class ReportModel
    {
        public DateTime Date { get; set; }
        public DateTime DateFrom { get; set; }=DateTime.Today.AddMonths(-1);
        public DateTime DateTo { get; set; }=DateTime.Today;
        public int Incomes { get; set; }
        public int Expenses { get; set; }
        public int Profit { get; set; }
        public IEnumerable<FinancialOperation> FinancialOperations { get; set; }
        public IEnumerable<UserOperation> UserOperations { get; set; }
    }
}
