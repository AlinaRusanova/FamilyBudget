using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Pages;
using FamilyFinance.UI.Service.Contracts;
using FamilyFinanceUI;

namespace FamilyFinance.UI.Service
{
    public class FinancialOperationService : IFinancialOperationService<FinancialOperationModel>
    {
        private readonly IHttpHandler _httpClient;
        public FinancialOperationService(IHttpHandler httpClient)
        {
            _httpClient = httpClient;
        }

        public List<FinancialOperationModel> ListOfEntities { get; set; } = new();

        public async Task GetListOfEntities()
        {
            var response = await _httpClient.GetAsync("/api/FinancialOperations/GetAllFO");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<BudgetItemModel>();
                }

                await SetEntities(response);

            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }

        }

        public async Task<FinancialOperationModel> GetEntity(int id)
        {
            var response = await _httpClient.GetAsync($"/api/FinancialOperations/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(FinancialOperationModel);
                }

                return await response.Content.ReadFromJsonAsync<FinancialOperationModel>();
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task AddEntity(FinancialOperationModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<FinancialOperationModel>("/api/FinancialOperations/AddFO", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<FinancialOperationModel>();
                }

                var list = await _httpClient.GetAsync("/api/FinancialOperations/GetAllFO");
                await SetEntities(list);
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task UpdateEntity(FinancialOperationModel model)
        {
            var response = await _httpClient.PutAsJsonAsync<FinancialOperationModel>($"/api/FinancialOperations/Update/", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<BudgetItemModel>();
                }

                var list = await _httpClient.GetAsync("/api/FinancialOperations/GetAllFO"); ;
                await SetEntities(list);

            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task DeleteEntity(FinancialOperationModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<FinancialOperationModel>("/api/FinancialOperations/Delete", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<FinancialOperationModel>();
                }

                var list = await _httpClient.GetAsync("/api/FinancialOperations/GetAllFO");
                await SetEntities(list);

            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }


        private async Task SetEntities(HttpResponseMessage result)
        {
            var response = await result.Content.ReadFromJsonAsync<List<FinancialOperationModel>>();
            ListOfEntities = response;
        }

    }
}
