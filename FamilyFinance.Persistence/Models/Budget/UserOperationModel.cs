using FamilyFinance.Domain.Entities.Budget;
using System.ComponentModel.DataAnnotations;

namespace FamilyFinance.Persistence.Models.Budget
{
    public class UserOperationModel : BaseTraceModel
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Today;
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Only positive number allowed.")]
        public int SumBudgetItem { get; set; }
        [RegularExpression(@"^[0-9]\d*$", ErrorMessage = "Only positive number allowed.")]
        public int SumFinOperation { get; set; }
        public int? BudgetItemId { get; set; } = 0;
        public int? FinOperationId { get; set; } = 0;

        public int UserId { get; set; }

        public BudgetItem? BudgetItem { get; set; }
        public FinancialOperation? FinOperation { get; set; }

    }
}
