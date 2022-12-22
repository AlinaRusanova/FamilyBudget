

namespace FamilyFinance.Persistence.Services.IServices.Identity
{
    public interface IUserService<A>
    {
        Task<A> Authenticate(A model, CancellationToken ct);
        Task<A> Register(A userModel, CancellationToken ct);

        Task<IEnumerable<A>> GetAllUsers(CancellationToken ct);
        Task<A> GetById(int id, CancellationToken ct);
    }
}
