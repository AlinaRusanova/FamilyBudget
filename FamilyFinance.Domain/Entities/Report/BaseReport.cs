using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Domain.Entities.Budget;

namespace FamilyFinance.Domain.Entities.Report
{
    public abstract class BaseReport : Entity
    {
        public int Incomes { get; set; }
        public int Expenses { get; set; }
        public int Profit { get; set; }  

        public IEnumerable<FinancialOperation> FinancialOperations { get; set; }
        public IEnumerable<UserOperation> UserOperations { get; set; }
    }
}
