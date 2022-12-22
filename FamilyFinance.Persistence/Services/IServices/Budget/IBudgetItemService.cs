namespace FamilyFinance.Persistence.Services.IServices.Budget
{
    public interface IBudgetItemService<M> : IAsyncService<M> where M : class, new()
    {
    }
}
