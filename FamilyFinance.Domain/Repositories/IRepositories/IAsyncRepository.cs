using FamilyFinance.Domain.Entities.Addition;

namespace FamilyFinance.Domain.Repositories.IRepositories
{
    public interface IAsyncRepository<T> where T : class, IEntity, new()
    {
        Task<T> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<T>> ListAllAsync(CancellationToken ct);
        Task<T> AddAsync(T entity, CancellationToken ct);
        Task<T> UpdateAsync(T entity, CancellationToken ct);
        Task DeleteAsync(T entity, CancellationToken ct);
    }
}
