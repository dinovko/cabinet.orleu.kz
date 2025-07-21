using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.cabinet.orleu.kz.Models
{
    public class CabinetUserEmployeeInform
    {
        [Key]
        public int Id { get; set; }
        public int EmpId { get; set; }
        public int? EmpOrganizationId { get; set; }
        public int? EmployeeDepartmentId { get; set; }
        public int? EmpPositionId { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual CabinetUser User { get; set; }
    }
}
