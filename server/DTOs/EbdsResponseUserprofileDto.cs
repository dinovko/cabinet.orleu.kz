using server.cabinet.orleu.kz.Models;

namespace server.cabinet.orleu.kz.DTOs
{
    public class EbdsResponseUserprofileDto
    {
        public string IIN { get; set; }
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
        public virtual EbdsResponseUserprofileEmployeeInformDto? EmployeeInform { get; set; }
        public virtual EbdsResponseUserprofileListenerInformDto? ListenerInform { get; set; }
        public virtual List<EbdsResponseUserprofileCoursesDto>? Courses { get; set; }
    }

    public class EbdsResponseUserprofileEmployeeInformDto
    {
        public int EmpId { get; set; }
        public int EmpOrganizationId { get; set; }
        public string? EmpOrganization { get; set; }
        public int? EmployeeDepartmentId { get; set; }
        public string? EmployeeDepartment { get; set; }
        public int? EmpPositionId { get; set; }
        public string? EmpPosition { get; set; }
    }

    public class EbdsResponseUserprofileListenerInformDto
    {
        public int ListenerJobId { get; set; }
        public string AreaCode { get; set; }
        public string RegionCode { get; set; }
        public int SchoolId { get; set; }
        public string School { get; set; }
        public string SchoolBIN { get; set; }
        public string? PedScienceDegreeId { get; set; }
        public string? PedQualCategoryId { get; set; }
        public string? PedSubjectId { get; set; }
        public string? PedPositionId { get; set; }
        public string? PedEducationTypeId { get; set; }
    }

    public class EbdsResponseUserprofileCoursesDto
    {
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string GroupCode { get; set; }
        public int GroupId { get; set; }
        public int CourseStatus { get; set; }
        public string CertificateNumber { get; set; }
        public string CertificateLink { get; set; }
        public DateTime? StartingDate { get; set; }
        public DateTime? EndDate { get; set; }

        /*
         нагрузка тренера по данному курсу, 
        если двухнедельные курсы 80 часов в общем, 
        у тренера стоит 20 часов, значить он преподает какой та раздел или модуль в этом курсе дегендей, 
        если хочешь отобразить в профиле тренера его нагрузки по курсам
         */
        public int? PedLoad { get; set; }
    }
}
