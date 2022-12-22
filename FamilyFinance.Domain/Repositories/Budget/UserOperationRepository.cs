using FamilyFinance.Domain.Entities.Budget;
using FamilyFinance.Domain.Repositories.IRepositories.Budget;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Domain.Repositories.Budget
{
    public class UserOperationRepository : BaseRepository<UserOperation>, IUserOperationRepository
    {
        public UserOperationRepository(FFDbContext dbContext) : base(dbContext) { }

        public async Task<IEnumerable<UserOperation>> ListAllAsync(CancellationToken ct)
        {
            return await _dbContext.Set<UserOperation>()
                .Include(s => s.BudgetItem)
                .Include(s => s.FinOperation)
                .Include(s=>s.User)
                .ToListAsync(ct);
        }

        public async Task<UserOperation> GetByIdAsync(int id, CancellationToken ct)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(UserOperationRepository));

            return await _dbContext.Set<UserOperation>()
                                    .Include(s => s.BudgetItem)
                                    .Include(s => s.FinOperation)
                                    .Include(s =>s.User)
                                    .FirstOrDefaultAsync(n => n.Id == id);
        }
    }
}
