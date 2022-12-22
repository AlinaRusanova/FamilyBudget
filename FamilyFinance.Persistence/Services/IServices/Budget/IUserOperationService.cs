namespace FamilyFinance.Persistence.Services.IServices.Budget
{
    public interface IUserOperationService<M> : IAsyncService<M> where M : class, new()
    {
        Task<IEnumerable<M>> ListAllAsync(int userId,CancellationToken ct);
    }
}