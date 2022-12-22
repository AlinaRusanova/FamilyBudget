using FamilyFinance.Domain.Entities.Addition;
using FamilyFinance.Domain.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace FamilyFinance.Domain.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T>, IDisposable where T : class, IEntity, new()
    {
        protected readonly FFDbContext _dbContext;
        bool _disposed;

        public BaseRepository(FFDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<T>> ListAllAsync(CancellationToken ct)
        {
            return await _dbContext.Set<T>().ToListAsync(ct);
        }

        public async Task<T> GetByIdAsync(int id, CancellationToken ct)
        {
            if (id < 1)
                throw new ArgumentOutOfRangeException(nameof(T));

            return await _dbContext.Set<T>().FirstOrDefaultAsync(n => n.Id == id);

        }

        public async Task<T> AddAsync(T entity, CancellationToken ct)
        {
            if (entity == null)
                throw new ArgumentOutOfRangeException(nameof(T));

            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync(ct);

            return entity;
        }

        public async Task<T> UpdateAsync(T entity, CancellationToken ct)
        {
            if (entity == null || entity.Id < 1)
                throw new ArgumentOutOfRangeException(nameof(T));

            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync(ct);
            return entity;
        }

        public async Task DeleteAsync(T entity, CancellationToken ct)
        {
            if (entity == null || entity.Id < 1)
                throw new ArgumentOutOfRangeException(nameof(T));

            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(ct);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
                _dbContext.Dispose();

            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
