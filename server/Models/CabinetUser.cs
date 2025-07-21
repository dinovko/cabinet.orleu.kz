using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace server.cabinet.orleu.kz.Models
{
    public class CabinetUser: IdentityUser<Guid>
    {
        [Required(ErrorMessage = "Длина ИИН должна составлять 12 символов")]
        public required string IIN { get; set; }
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string NationalityId { get; set; }
        public string GenderId { get; set; }
        public int TotalEx { get; set; }
        public int PedExper { get; set; }
        public string BirthDate { get; set; } // Или DateTime, если будешь парсить дату
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool IsEmployee { get; set; }
        public virtual CabinetUserEmployeeInform EmployeeInform { get; set; }
        public virtual CabinetUserListenerInform ListenerInform { get; set; }
        public virtual List<CabinetUserCourse> Courses { get; set; }

        public DateTimeOffset LastUpdate { get; set; } = DateTimeOffset.UtcNow;
    }

    public class CabinetRole : IdentityRole<Guid> 
    {

    }
}
