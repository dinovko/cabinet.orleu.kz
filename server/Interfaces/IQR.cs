using server.cabinet.orleu.kz.DTOs;

namespace server.cabinet.orleu.kz.Interfaces
{
    public interface IQR
    {
        Task<QrGroupsDto> GetGroups(string iin, int? page = 1, int? size = 20);
    }
}
