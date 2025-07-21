using server.cabinet.orleu.kz.DTOs;

namespace server.cabinet.orleu.kz.Interfaces
{
    public interface IEbds
    {
        Task<EbdsResponseUserprofileDto> GetResponseUserprofile(string iin);
    }
}
