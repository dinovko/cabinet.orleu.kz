export interface IGroup {
  groups: GroupDto[];
  total_groups: number;
  pagination: PaginationDto;
  filter_options: FilterOptionsDto;
  participant_info: ParticipantInfoDto;
}

export interface GroupDto {
  id: number;
  code: string;
  course_name: string;
  start_date: string; // ISO string: "2025-07-11"
  end_date: string;   // ISO string: "2025-07-18"
  sessions_count: number;
  sessions: SessionDto[];
}

export interface SessionDto {
  id: number;
  date: string; // ISO string: "2025-07-15"
  is_today: boolean;
  attendance: AttendanceDto | null;
}

export interface AttendanceDto {
  id: number;
  arrived_at: string; // ISO string with timezone
  arrived_status: string;
  arrived_status_display: string;
  left_at: string; // ISO string with timezone
  left_status: string;
  left_status_display: string;
  trust_level: string;
  trust_level_display: string;
  trust_score: number;
  marked_entry_by_trainer: boolean;
  marked_exit_by_trainer: boolean;
}

export interface PaginationDto {
  page: number;
  per_page: number;
  total_pages: number;
  total_items: number;
}

export interface FilterOptionsDto {
  arrival_statuses: StatusOptionDto[];
  trust_levels: StatusOptionDto[];
}

export interface StatusOptionDto {
  value: string;
  display: string;
}

export interface ParticipantInfoDto {
  iin: string;
  full_name: string;
  role: string;
}
