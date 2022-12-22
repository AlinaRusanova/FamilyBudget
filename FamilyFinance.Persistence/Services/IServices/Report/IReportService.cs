namespace FamilyFinance.Persistence.Services.IServices.Report
{
    public interface IReportService<M> where M : class, new()
    {
        Task<M> GetDailyReportAsync(DateTime date, int id, CancellationToken ct);
        Task<M> GetPeriodReportAsync(DateTime dateFrom, DateTime dateTo, int id, CancellationToken ct);
    }
}
