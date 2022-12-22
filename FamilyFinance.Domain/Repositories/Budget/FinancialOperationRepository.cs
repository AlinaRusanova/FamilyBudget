using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;

namespace FamilyFinance.Domain.Repositories.Budget
{
    public class FinancialOperationRepository : BaseRepository<FinancialOperation>, IFinancialOperationRepository
    {
        public FinancialOperationRepository(FFDbContext dbContext) : base(dbContext) { }
    }
}
