using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Pages;
using FamilyFinance.UI.Service.Contracts;
using FamilyFinanceUI;
using System.Text.RegularExpressions;

namespace FamilyFinance.UI.Service
{
    public class ReportService : IReportService<ReportModel>
    {
        private readonly IHttpHandler _httpClient;
        private readonly ILogger _logger;
        public ReportService(IHttpHandler httpClient, ILoggerFactory loggerFactory)
        {
            _httpClient = httpClient;
            _logger = loggerFactory.CreateLogger<ReportService>();
        }
        public async Task<ReportModel> GetDailyReportAsync(string input)
        {
            _logger.LogInformation("GetDailyReportAsync invoke 1, input:" + input);

            var strigDate = DateStringConvert(input);

            _logger.LogInformation("GetDailyReportAsync invoke 2, input:"+ strigDate);
            var response = await _httpClient.GetAsync($"/api/Report/GetDailyReport/{strigDate}");

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
                _logger.LogError("GetDailyReportAsync invoke"+message.Error);
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task<ReportModel> GetPeriodReportAsync(string inputFrom, string inputTo)
        {
            var dateFrom = DateStringConvert(inputFrom);
            var dateTo = DateStringConvert(inputTo);

            var response = await _httpClient.GetAsync($"/api/Report/GetPeriodReport/{dateFrom}/{dateTo}");

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
                _logger.LogError(message.Error);
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        private string DateStringConvert(string input)
        {
            string pattern = "/";
            string replacement = ".";

            var strigDate = Regex.Replace(input, pattern, replacement);

            return strigDate;
        }
    }
}
