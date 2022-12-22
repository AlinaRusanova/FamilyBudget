using FamilyFinance.Domain.Entities.Identity;
using FamilyFinance.Domain.Repositories.IRepositories.Identity;

namespace FamilyFinance.Domain.Repositories.Identity
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(FFDbContext dbContext) : base(dbContext) { }
    }
}
