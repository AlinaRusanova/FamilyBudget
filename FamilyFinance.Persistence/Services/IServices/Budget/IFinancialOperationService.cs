namespace FamilyFinance.Persistence.Services.IServices.Budget
{
    public interface IFinancialOperationService<M> : IAsyncService<M> where M : class, new()
    {
    }
}
