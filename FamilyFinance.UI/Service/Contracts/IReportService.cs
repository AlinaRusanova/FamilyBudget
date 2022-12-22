namespace FamilyFinance.UI.Service.Contracts
{
    public interface IReportService<M> where M : class, new()
    {
        Task<M> GetDailyReportAsync(string date);
        Task<M> GetPeriodReportAsync(string dateFrom, string dateTo);
    }
}
