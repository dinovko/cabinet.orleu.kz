using System.ComponentModel.DataAnnotations;

namespace server.cabinet.orleu.kz.Models
{
    public class refNobdschool
    {
        [Key]
        public int schoolId { get; set; }
        public string? areacode { get; set; }
        public string? regioncode { get; set; }
        public string? localitycode { get; set; }
        public string rname { get; set; }
        public string kname { get; set; }
        public string? bin { get; set; }
    }
}
