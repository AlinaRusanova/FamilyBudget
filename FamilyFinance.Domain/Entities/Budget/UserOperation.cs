using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Domain.Entities.Identity;

namespace FamilyFinance.Domain.Entities.Budget
{
    public class UserOperation : Entity
    {
        public DateTime Date { get; set; } = DateTime.Today.Date;
        public BudgetItem? BudgetItem { get; set; }
        public int SumBudgetItem { get; set; } = 0;
        public FinancialOperation? FinOperation { get; set; }
        public int SumFinOperation { get; set; } = 0;
        public int? BudgetItemId { get; set; } = null;
        public int? FinOperationId { get; set; } = null;

        public User? User { get; set; }
        public int UserId { get; set; }
       
    }
}
