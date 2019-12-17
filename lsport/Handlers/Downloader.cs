using System.Net.Http;
using System.Threading.Tasks;
using lsport.Interfaces;

namespace lsport.Handlers
{
    public class Downloader : IDownloader
    {
        private readonly IHttpClientFactory _clientFactory;

        public Downloader(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        private static HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            var request = new HttpRequestMessage(method, url);
            return request;
        }

        public async Task<string> Download(string url)
        {
            var request = CreateRequest(HttpMethod.Get, url);
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(request);
            return await response.Content.ReadAsStringAsync();
        }
    }
}