using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FamilyFinanceUI
{
    public interface IHttpHandler
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsJsonAsync<M>(string url, M model);
        Task<HttpResponseMessage> PutAsJsonAsync<M>(string url, M model);

    }
}
