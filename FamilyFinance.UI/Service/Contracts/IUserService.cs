namespace FamilyFinance.UI.Service.Contracts
{
    public interface IUserService<M>
    {
        Task<M> Register(M model);
        Task<M> Login(M model);
    }
}
