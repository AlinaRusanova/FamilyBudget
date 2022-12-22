using FamilyFinance.Persistence.Models.Identity;
using FamilyFinance.UI.Pages;
using FamilyFinance.UI.Service.Contracts;
using FamilyFinanceUI;
using Microsoft.AspNetCore.Components;
using System.Net.Http;

namespace FamilyFinance.UI.Service
{
    public class UserService : IUserService<UserModel>
    {
        private readonly IHttpHandler _httpClient;
        public UserService(IHttpHandler httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<UserModel> Register(UserModel model)
        {
                var response = await _httpClient.PostAsJsonAsync<UserModel>("/api/User/register", model);
                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        Enumerable.Empty<UserModel>();
                    }

                    return await response.Content.ReadFromJsonAsync<UserModel>();
                }
                else
                {                     
                    var error = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                    throw new Exception(error.Error);
                }       
        }

        public async Task<UserModel> Login(UserModel model)
        {
            var response = await _httpClient.PostAsJsonAsync<UserModel>("/api/User/authenticate", model);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<UserModel>();
            }

            else
            {
                var error = await response.Content.ReadFromJsonAsync<ErrorMessage>();
                throw new Exception(error.Error);
            }
        }
    }
}
