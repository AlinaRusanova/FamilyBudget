using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Pages;
using FamilyFinance.UI.Service.Contracts;
using FamilyFinanceUI;

namespace FamilyFinance.UI.Service
{
    public class BudgetItemService : IBudgetItemService<BudgetItemModel>
    {
        private readonly IHttpHandler _httpClient;
        public BudgetItemService(IHttpHandler httpClient)
        {
            _httpClient = httpClient;
        }

        public List<BudgetItemModel> ListOfEntities { get; set; } = new();

        public async Task GetListOfEntities()
        {
            var response = await _httpClient.GetAsync("api/BudgetItems/GetAllBI");
            
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

        public async Task<BudgetItemModel> GetEntity(int id)
        {
            var response = await _httpClient.GetAsync($"/api/BudgetItems/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(BudgetItemModel);
                }

                return await response.Content.ReadFromJsonAsync<BudgetItemModel>();
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }

        }

        public async Task AddEntity(BudgetItemModel model)
        {

            var response = await _httpClient.PostAsJsonAsync<BudgetItemModel>("/api/BudgetItems/AddBI", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<BudgetItemModel>();
                }

                var list = await _httpClient.GetAsync("/api/BudgetItems/GetAllBI");
                await SetEntities(list);
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task UpdateEntity(BudgetItemModel model)
        {
            var response = await _httpClient.PutAsJsonAsync<BudgetItemModel>($"/api/BudgetItems/UpdateBI/", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<BudgetItemModel>();
                }

                var list = await _httpClient.GetAsync("/api/BudgetItems/GetAllBI"); ;
                await SetEntities(list);

            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task DeleteEntity(BudgetItemModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<BudgetItemModel>("/api/BudgetItems/DeleteBI", model);

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<BudgetItemModel>();
                }

                var list = await _httpClient.GetAsync("/api/BudgetItems/GetAllBI");
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
            var response = await result.Content.ReadFromJsonAsync<List<BudgetItemModel>>();
            ListOfEntities = response;
        }

    }
}
