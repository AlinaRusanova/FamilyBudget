

namespace FamilyFinanceUI
{
    public class HttpHandler: IHttpHandler
    {
        private HttpClient _client;
        public HttpHandler(HttpClient client)
        {
            _client = client;
        }
        

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await _client.GetAsync(url);
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<M>(string url, M model)
        {
            return await _client.PostAsJsonAsync<M>(url, model);
        }

        public async Task<HttpResponseMessage> PutAsJsonAsync<M>(string url, M model)
        {
            return await _client.PutAsJsonAsync<M>(url, model);
        }
    }
}
 