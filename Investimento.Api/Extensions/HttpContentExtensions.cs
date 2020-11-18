
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Investimento.Api.Extensions
{
    public static class HttpContentExtensions
    {
        public static async Task<T> ReadAsJsonAsync<T>(this HttpContent httpContent) where T : class
        {
            return JsonConvert.DeserializeObject<T>(await httpContent.ReadAsStringAsync());
        }
    }
}
