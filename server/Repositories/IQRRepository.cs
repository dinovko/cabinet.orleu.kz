using server.cabinet.orleu.kz.DTOs;
using server.cabinet.orleu.kz.Interfaces;
using server.cabinet.orleu.kz.Services;

namespace server.cabinet.orleu.kz.Repositories
{
    public class IQRRepository : IQR
    {
        private readonly IHttpService _httpService;
        private readonly IConfiguration _configuration;
        private readonly string _token;
        private readonly string _url;
        public IQRRepository(IHttpService httpService, IConfiguration configuration)
        {
            _httpService = httpService;
            _configuration = configuration;

            _token = _configuration["qr:token"] ?? "";
            _url = _configuration["qr:url_my_groups"] ?? "https://qr.odx.kz/api/groups/my_groups/?participant_iin=";
        }
        public async Task<QrGroupsDto> GetGroups(string iin, int? page = 1, int? size = 20)
        {
            string requestURL = $"{_url}{iin}";
            var result = await _httpService.GetAsync<QrGroupsDto>(requestURL, _token);

            if(result == null)
            {
                return new QrGroupsDto()
                {
                    Groups = Array.Empty<GroupDto>().ToList()
                };
            }

            return result;
        }
    }
}
