
namespace FamilyFinance.UI.Service.Contracts
{
    public interface ICommonService<T> where T : class, new()
    {
        List<T> ListOfEntities { get; set; }

        Task GetListOfEntities();
        Task<T> GetEntity(int id);
        Task AddEntity(T model);
        Task UpdateEntity(T model);
        Task DeleteEntity(T model);
    }
}
