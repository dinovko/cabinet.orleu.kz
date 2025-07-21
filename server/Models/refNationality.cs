using System.ComponentModel.DataAnnotations;

namespace server.cabinet.orleu.kz.Models
{
    public class refNationality
    {
        [Key]
        public int  Id { get; set; }
        public string pedNationId { get; set; }
        public int empNationId { get; set; }
        public string nameRU { get; set; }
        public string nameKZ { get; set; }
    }
}
