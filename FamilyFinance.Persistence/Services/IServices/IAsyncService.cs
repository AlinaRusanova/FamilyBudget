
namespace FamilyFinance.Persistence.Services.IServices
{
    public interface IAsyncService<M> where M : class, new()
    {
        Task<M> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<M>> ListAllAsync(CancellationToken ct);
        Task<M> AddAsync(M entity, CancellationToken ct);
        Task<M> UpdateAsync(M entity, CancellationToken ct);
        Task DeleteAsync(M entity, CancellationToken ct);
    }
}
