using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.cabinet.orleu.kz.Models
{
    public class CabinetUserListenerInform
    {
        [Key]
        public int Id { get; set; }
        public int ListenerJobId { get; set; }
        public string AreaCode { get; set; }
        public string RegionCode { get; set; }
        public int SchoolId { get; set; }
        public string SchoolBIN { get; set; }
        public int? PedScienceDegreeId { get; set; }
        public int? PedQualCategoryId { get; set; }
        public int? PedSubjectId { get; set; }
        public int? PedPositionId { get; set; }
        public int? PedEducationTypeId { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual CabinetUser User { get; set; }
    }
}
