using System.ComponentModel.DataAnnotations;

namespace server.cabinet.orleu.kz.Models
{
    public class refNobdarea
    {
        [Key]
        public string code { get; set; }
        public string rname { get; set; }
        public string kname { get; set; }
    }
}
