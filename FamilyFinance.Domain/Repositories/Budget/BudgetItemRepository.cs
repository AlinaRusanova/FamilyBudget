using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;

namespace FamilyFinance.Domain.Repositories.Budget
{
    public class BudgetItemRepository : BaseRepository<BudgetItem>, IBudgetItemRepository
    {
        public BudgetItemRepository(FFDbContext dbContext) : base(dbContext) { }

    }
}
