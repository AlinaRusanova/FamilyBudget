using FamilyFinance.Domain.Entities.Addition;

namespace FamilyFinance.Persistence.Models.Budget
{
    public class BudgetItemModel : BaseTraceModel
    {

        public BudgetType BudgetType { get; set; }
        public string Item { get; set; }
    }
}
