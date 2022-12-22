
namespace FamilyFinance.UI.Service.Contracts
{
    public interface IFinancialOperationService<T> : ICommonService<T> where T : class, new()
    {
    }
}
