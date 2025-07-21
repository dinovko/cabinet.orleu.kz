using System.ComponentModel.DataAnnotations;

namespace server.cabinet.orleu.kz.Models
{
    public class refOrleubranch
    {
        [Key]
        public int id { get; set; }
        public string nameru { get; set; }
        public string namekz { get; set; }
        public string bin { get; set; }
    }
}
