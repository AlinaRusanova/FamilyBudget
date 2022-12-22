using FamilyFinance.Domain.Entities.Addition;

namespace FamilyFinance.Domain.Entities.Budget
{
    public class BudgetItem : Entity
    {
        public BudgetType BudgetType { get; set; }
        public string Item { get; set; }
    }
}
