using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace server.cabinet.orleu.kz.Models
{
    public class CabinetUserCourse
    {
        [Key]
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public string GroupCode { get; set; }
        public int GroupId { get; set; }
        public int CourseStatus { get; set; }
        public string? CertificateNumber { get; set; }
        public string? CertificateLink { get; set; }
        public DateTimeOffset StartingDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public int PedLoad { get; set; }

        public Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual CabinetUser User { get; set; }

    //    [NotMapped]
    //    public DateTime StartingDateLocal =>
    //TimeZoneInfo.ConvertTimeFromUtc(
    //    StartingDate.UtcDateTime,
    //    TimeZoneInfo.FindSystemTimeZoneById("Qyzylorda Standard Time"));

    //    [NotMapped]
    //    public DateTime? EndDateLocal =>
    //        EndDate.HasValue
    //            ? TimeZoneInfo.ConvertTimeFromUtc(
    //                  EndDate.Value.UtcDateTime,
    //                  TimeZoneInfo.FindSystemTimeZoneById("Qyzylorda Standard Time"))
    //            : null;
    }
}
