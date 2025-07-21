namespace server.cabinet.orleu.kz.DTOs
{
    public class QrGroupsDto
    {
        public List<GroupDto> Groups { get; set; }
        public int TotalGroups { get; set; }
        public PaginationDto Pagination { get; set; }
        public FilterOptionsDto FilterOptions { get; set; }
        public ParticipantInfoDto ParticipantInfo { get; set; }
    }

    public class GroupDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CourseName { get; set; }
        public DateTime Start_date { get; set; }
        public DateTime End_date { get; set; }
        public int SessionsCount { get; set; }
        public List<SessionDto>? Sessions { get; set; }
    }

    public class SessionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public bool IsToday { get; set; }
        public AttendanceDto? Attendance { get; set; }
    }

    public class AttendanceDto
    {
        public int Id { get; set; }
        public DateTime Arrived_at { get; set; }
        public string Arrived_status { get; set; }
        public string Arrived_status_display { get; set; }
        public DateTime? Left_at { get; set; }
        public string Left_status { get; set; }
        public string Left_status_display { get; set; }
        public string Trust_level { get; set; }
        public string Trust_level_display { get; set; }
        public int Trust_score { get; set; }
        public bool Marked_entry_by_trainer { get; set; }
        public bool Marked_exit_by_trainer { get; set; }
    }

    public class PaginationDto
    {
        public int Page { get; set; }
        public int PerPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
    }

    public class FilterOptionsDto
    {
        public List<StatusOptionDto> ArrivalStatuses { get; set; }
        public List<StatusOptionDto> TrustLevels { get; set; }
    }

    public class StatusOptionDto
    {
        public string Value { get; set; }
        public string Display { get; set; }
    }

    public class ParticipantInfoDto
    {
        public string Iin { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; }
    }
}
