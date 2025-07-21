using server.cabinet.orleu.kz.DTOs;
using System.Text.Json;

namespace server.cabinet.orleu.kz.Services
{
    public class QrApiService
    {
        private readonly ILogger<QrApiService> _logger;

        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private string URLMyGroups = string.Empty;
        private string ApiToken = string.Empty;



        public QrApiService(
            ILogger<QrApiService> logger,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _configuration = configuration;
            _logger = logger;

            ApiToken = configuration["qr:token"];
            URLMyGroups = configuration["qr:url_my_groups"];

            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(URLMyGroups);

            _httpClient.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", ApiToken);
        }

        public async Task<QrGroupsDto> GetDataAsync(string iin, int page,int size)
        {
            iin = "920318350760";
            var response = await _httpClient.GetAsync($"api/groups/my_groups/?participant_iin={iin}&page={page}&per_page={size}");

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"Ошибка при получении данных из QR (Код ошибки:{response.StatusCode})");
                return new QrGroupsDto()
                {
                    Groups = Array.Empty<GroupDto>().ToList()
                };
            }
            //response.EnsureSuccessStatusCode();

                var stream = await response.Content.ReadAsStreamAsync();

                var options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    PropertyNameCaseInsensitive = true
                };

                var dto = await JsonSerializer.DeserializeAsync<QrGroupsDto>(stream, options);

                return dto;
             
        }
    }
}
