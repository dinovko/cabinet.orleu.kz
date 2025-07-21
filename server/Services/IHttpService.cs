using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;

namespace server.cabinet.orleu.kz.Services
{
    public interface IHttpService
    {
        Task<T> GetAsync<T>(string uri, string token = null);
        Task<T> PostAsync<T>(string uri, object data, string token = null);
        //Task<T> PutAsync<T>(string uri, object data, string token = null);
        //Task<bool> DeleteAsync(string uri, string token = null);
    }
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public HttpService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;

            // Базовый адрес API из appsettings.json
            //_httpClient.BaseAddress = new Uri(_config["ApiBaseUrl"]);
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //public Task<bool> DeleteAsync(string uri, string token = null)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<T> GetAsync<T>(string uri, string token = null)
        {
            var request = CreateRequest(HttpMethod.Get, uri, token);
            return await ExecuteRequest<T>(request);
        }

        public async Task<T> PostAsync<T>(string uri, object data, string token = null)
        {
            var request = CreateRequest(HttpMethod.Post, uri, token, data);
            return await ExecuteRequest<T>(request);
        }

        //public Task<T> PutAsync<T>(string uri, object data, string token = null)
        //{
        //    throw new NotImplementedException();
        //}

        private HttpRequestMessage CreateRequest(HttpMethod method, string uri, string token = null, object data = null)
        {
            var request = new HttpRequestMessage(method, uri);

            if (!string.IsNullOrEmpty(token))
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (data != null)
                request.Content = new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json");

            return request;
        }

        private async Task<T> ExecuteRequest<T>(HttpRequestMessage request)
        {
            try
            {
                var response = await _httpClient.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (HttpRequestException ex)
            {
                return default; // Вернет null для ссылочных типов
            }
            catch (JsonException ex)
            {
                return default; // Ошибка десериализации
            }
            catch (Exception ex)
            {
                return default; // Любые другие ошибки
            }
        }
    }
}
