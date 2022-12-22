using FamilyFinance.Persistence.Models.Budget;
using FamilyFinance.UI.Pages;
using FamilyFinance.UI.Service.Contracts;
using FamilyFinanceUI;
using Microsoft.AspNetCore.Components;

namespace FamilyFinance.UI.Service
{
    public class UserOperationService : IUserOperationsService<UserOperationModel>
    {
        private readonly IHttpHandler _httpClient;
        public UserOperationService(IHttpHandler httpClient)
        {
            _httpClient = httpClient;
        }

        public List<UserOperationModel> ListOfEntities { get; set; } = new();

        public async Task GetListOfEntities()
        {
            var response = await _httpClient.GetAsync("/api/UserOperations/GetAllUO");

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

        public async Task<UserOperationModel> GetEntity(int id)
        {
            var response = await _httpClient.GetAsync($"/api/UserOperations/{id}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    return default(UserOperationModel);
                }

                return await response.Content.ReadFromJsonAsync<UserOperationModel>();
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task AddEntity(UserOperationModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<UserOperationModel>("/api/UserOperations/AddUO", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<UserOperationModel>();
                }

                var list = await _httpClient.GetAsync("/api/UserOperations/GetAllUO");
                await SetEntities(list);
            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task UpdateEntity(UserOperationModel model)
        {
            var response = await _httpClient.PutAsJsonAsync<UserOperationModel>($"/api/UserOperations/Update/", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<BudgetItemModel>();
                }

                var list = await _httpClient.GetAsync("/api/UserOperations/GetAllUO"); ;
                await SetEntities(list);

            }
            else
            {
                var message = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception($"Http status:{response.StatusCode} Message - {message.Error}");
            }
        }

        public async Task DeleteEntity(UserOperationModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<UserOperationModel>("/api/UserOperations/Delete", model);
            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    Enumerable.Empty<UserOperationModel>();
                }

                var list = await _httpClient.GetAsync("/api/UserOperations/GetAllUO");
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
            var response = await result.Content.ReadFromJsonAsync<List<UserOperationModel>>();
            ListOfEntities = response;
        }

    }
}
