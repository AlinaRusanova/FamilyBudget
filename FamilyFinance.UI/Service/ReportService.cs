using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Pages;
using FamilyFinance.UI.Service.Contracts;
using FamilyFinanceUI;

namespace FamilyFinance.UI.Service
{
    public class ReportService : IReportService<ReportModel>
    {
        private readonly IHttpHandler _httpClient;
        public ReportService(IHttpHandler httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<ReportModel> GetDailyReportAsync(string input)
        {
            var response = await _httpClient.GetAsync($"/api/Report/GetDailyReport/{input}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<ReportModel>();
                }

                return await response.Content.ReadFromJsonAsync<ReportModel>();

            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task<ReportModel> GetPeriodReportAsync(string inputFrom, string inputTo)
        {
            var response = await _httpClient.GetAsync($"/api/Report/GetPeriodReport/{inputFrom}/{inputTo}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<ReportModel>();
                }

                return await response.Content.ReadFromJsonAsync<ReportModel>();
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }
    }
}
